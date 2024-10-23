using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class MediaRepository(AppDbContext db) : IMediaRepository
{
    private const int FiveMegaBytes = 5 * 1024 * 1024;
    private const string AcceptedFileSizeMessage = "Accepted file size was 5mb.";

    public async Task<PagedResult<MediaInfo>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm, AssetType? assetType)
    {
        IQueryable<Storage> query = db.Storages;

        if (assetType.HasValue)
        {
            query = query.Where(u => u.AssetType == assetType);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.Description.Contains(searchTerm) ||
                                     u.Name.Contains(searchTerm));
        }

        PagedResult<MediaInfo> pagedResult = await query
            .OrderByDescending(u => u.CreatedOn)
            .Select(u => new MediaInfo
            {
                Id = u.Id,
                Name = u.Name,
                AssetType = u.AssetType,
                ContentType = u.ContentType,
                DeletedAt = u.DeletedAt,
                IsDeleted = u.IsDeleted,
                Length = u.Length,
                Path = u.Path,
                Description = u.Description
            })
            .ToPagedListAsync(pageNumber, pageSize);

        return pagedResult;
    }

    public string[] ValidateFiles(List<IFormFile> files)
    {
        List<string>? invalidFiles = files.Where(file => InvalidFileType(file.FileName)).Select(file => $"Invalid file type: {file.FileName}.").ToList();

        List<string>? invalidSizedFiles = files.Where(file => InvalidFileType(file.FileName)).Select(file => $"File {file.FileName} was too large. Accepted file size was 5mb.").ToList();

        if (invalidFiles?.Count == 0) invalidFiles = [];

        if (invalidSizedFiles?.Count == 0) invalidSizedFiles = [];

        return [.. invalidFiles, .. invalidSizedFiles];
    }

    [HttpPost]
    public async Task<ReturnResult<string[]>> Upload(List<IFormFile> files, int LoggedInUserId)
    {
        List<Storage> storages = [];

        //TODO: get from db
        MediaSettings mediaOptions = new MediaSettings();

        foreach (IFormFile file in files)
        {
            string path = string.Empty;

            if (mediaOptions.OrganizeUploads)
            {
                path = Path.Combine($"{DateTime.Now.Year}", $"{DateTime.Now.Month}");
            }

            string extension = Path.GetExtension(file.FileName);
            string orgFileName = GetFileName(file.FileName).Replace(extension, string.Empty);
            string fullPath = Path.Combine("wwwroot", "content", "media", path);
            string fullDirectoryPath = Path.Combine(AppConstants.ContentRootPath, fullPath);

            Directory.CreateDirectory(fullDirectoryPath);

            string uniqueFileName = GetFileSlug(fullDirectoryPath, orgFileName, extension);

            if (IsImage(file.FileName))
            {
                List<Storage> filesData = await ImageOperations(file, extension, fullPath, uniqueFileName, orgFileName, mediaOptions, LoggedInUserId);
                storages.AddRange(filesData);
            }
            else
            {
                string fullFilePath = Path.Combine(fullDirectoryPath, $"{uniqueFileName}{extension}");
                await using FileStream fileStream = new(fullFilePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                storages.Add(CreateStorageEntry(file, orgFileName, fullFilePath, LoggedInUserId));
            }
        }

        await db.Storages.AddRangeAsync(storages);
        await db.SaveChangesAsync();

        return new ReturnResult<string[]> { Message = $"{files.Count} uploaded successfully.", Code = StatusCodes.Status200OK };
    }

    public async Task<ReturnResult<int>> Delete(List<int> files, bool isPermanent)
    {
        IQueryable<Storage> storages = db.Storages.Where(s => files.Contains(s.Id));

        int count = await storages.CountAsync();

        await foreach (Storage? file in storages.AsAsyncEnumerable())
        {
            string filePath = AppConstants.WebRootPath + file.Path;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await storages.ExecuteDeleteAsync();

        return new ReturnResult<int>
        {
            Message = $"Deleted {count} files successfully.",
            Code = StatusCodes.Status200OK,
            Result = count,
            Title = "Media.Deleted"
        };
    }

    private async Task<List<Storage>> ImageOperations(IFormFile file, string extension, string fullPath, string uniqueFileName, string orgFileName, MediaSettings mediaOptions, int LoggedInUserID)
    {
        await using MemoryStream memoryStream = new();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        ImageOptimizer optimizer = new() { IgnoreUnsupportedFormats = true };
        optimizer.LosslessCompress(memoryStream);

        using MagickImage originalImage = new(memoryStream);

        MagickFormat format = extension.ToLower() switch
        {
            ".png" => MagickFormat.Png,
            ".jpg" or ".jpeg" => MagickFormat.Jpeg,
            ".webp" => MagickFormat.WebP,
            _ => MagickFormat.Jpeg
        };

        List<Storage> storages = [];

        string originalPath = Path.Combine(AppConstants.ContentRootPath, fullPath, $"{uniqueFileName}{extension}");

        int Quality = mediaOptions.OptimizeImages ? mediaOptions.OptimizedQuality.HasValue && mediaOptions.OptimizedQuality > 0 ? mediaOptions.OptimizedQuality.Value : 100 : 100;

        await SaveImage(originalImage, originalPath, format, Quality, false);

        storages.Add(CreateStorageEntry(file, orgFileName, originalPath, LoggedInUserID));

        if (mediaOptions.SmallSize.MaxWidth > 0 && mediaOptions.SmallSize.MaxHeight > 0)
        {
            string resizedThumbnailPath = await ResizeAndCropImage(originalImage, true, fullPath, uniqueFileName, extension, format, mediaOptions.SmallSize, Quality);
            storages.Add(CreateStorageEntry(file, orgFileName, resizedThumbnailPath, LoggedInUserID));
        }

        if (mediaOptions.MediumSize.MaxWidth > 0 && mediaOptions.MediumSize.MaxHeight > 0)
        {
            string resizedMediumPath = await ResizeAndCropImage(originalImage, false, fullPath, uniqueFileName, extension, format, mediaOptions.MediumSize, Quality);
            storages.Add(CreateStorageEntry(file, orgFileName, resizedMediumPath, LoggedInUserID));
        }

        if (mediaOptions.LargeSize.MaxWidth > 0 && mediaOptions.LargeSize.MaxHeight > 0)
        {
            string resizedLargePath = await ResizeAndCropImage(originalImage, false, fullPath, uniqueFileName, extension, format, mediaOptions.LargeSize, Quality);
            storages.Add(CreateStorageEntry(file, orgFileName, resizedLargePath, LoggedInUserID));
        }

        return storages;
    }

    private static async Task<string> ResizeAndCropImage(MagickImage original, bool autoCrop, string fullPath, string uniqueFileName, string extension, MagickFormat format, MediaSize size, int Quality)
    {
        using MagickImage? image = original.Clone() as MagickImage;
        ArgumentNullException.ThrowIfNull(image);

        long targetWidth = Math.Min(image.Width, size.MaxWidth);
        long targetHeight = Math.Min(image.Height, size.MaxHeight);

        if (autoCrop)
        {
            image.Resize(new MagickGeometry((uint)targetWidth, (uint)targetHeight) { FillArea = true });
            long offsetX = (image.Width - targetWidth) / 2;
            long offsetY = (image.Height - targetHeight) / 2;
            image.Crop(new MagickGeometry((uint)targetWidth, (uint)targetHeight) { X = (int)offsetX, Y = (int)offsetY });
        }
        else
        {
            image.Resize((uint)targetWidth, (uint)targetHeight);
        }

        string resizedPath = Path.Combine(AppConstants.ContentRootPath, fullPath, $"{uniqueFileName}-{image.Width}x{image.Height}{extension}");

        await SaveImage(image, resizedPath, format, Quality, true);

        return resizedPath;
    }

    private static async Task SaveImage(MagickImage image, string path, MagickFormat format, int Quality, bool optimiseBeforeSaving = true)
    {
        if (optimiseBeforeSaving)
        {
            image.Strip();
            OptimizeImage(image, format, Quality);
        }

        await image.WriteAsync(path);
    }

    private static void OptimizeImage(MagickImage image, MagickFormat format, int Quality)
    {
        switch (format)
        {
            case MagickFormat.Jpeg:
                OptimizeJpeg(image, Quality);
                break;
            case MagickFormat.Png:
                OptimizePng(image, Quality);
                break;
            case MagickFormat.WebP:
                OptimizeWebP(image, Quality);
                break;
        }
    }

    private static void OptimizeJpeg(MagickImage image, int Quality)
    {
        image.Format = MagickFormat.Jpeg;
        image.Settings.Compression = CompressionMethod.JPEG;
        image.Quality = (uint)Quality;
        image.Settings.SetDefine("jpeg:interlace", "plane");
        image.Settings.SetDefine("jpeg:sampling-factor", "4:2:0");
        image.Settings.SetDefine("jpeg:dct-method", "float");
        image.Settings.SetDefine("jpeg:optimize-coding", "true");
    }

    private static void OptimizePng(MagickImage image, int Quality)
    {
        image.Strip();
        image.Format = MagickFormat.Png;
        image.Settings.Compression = CompressionMethod.Zip;
        image.Quality = (uint)Quality;
        image.Settings.SetDefine("png:compression-level", "9");
        image.Settings.SetDefine("png:compression-strategy", "2");
        image.Settings.SetDefine("png:exclude-chunk", "all");
        image.Settings.SetDefine("png:include-chunk", "tRNS,pHYs");
    }

    private static void OptimizeWebP(MagickImage image, int Quality)
    {
        image.Format = MagickFormat.WebP;
        image.Settings.Compression = CompressionMethod.WebP;
        image.Quality = (uint)Quality;
        image.Settings.SetDefine(MagickFormat.WebP, "lossless", "false");
        image.Settings.SetDefine(MagickFormat.WebP, "method", "6");
    }

    private Storage CreateStorageEntry(IFormFile file, string originalFileName, string filePath, int LoggedInUserID)
    {
        return new()
        {
            ContentType = file.ContentType,
            CreatedUserId = LoggedInUserID,
            CreatedOn = DateTime.UtcNow,
            Length = file.Length,
            Name = originalFileName,
            Path = TrimFilePath(filePath),
            AssetType = GetAssetType(Path.GetExtension(file.FileName)),
            IsDeleted = false
        };
    }

    private static bool InvalidFileType(string fileName)
    {
        return !AppConstants.FileExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsImage(string fileName)
    {
        return AppConstants.ImageExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }

    private static string TrimFilePath(string path)
    {
        string p = path.Replace(AppConstants.WebRootPath, "");
        //p = p.Replace(Path.DirectorySeparatorChar.ToString(), "/");
        return p;
    }

    private static string GetFileName(string fileName)
    {
        string separator = Path.DirectorySeparatorChar.ToString();
        if (fileName.Contains(separator))
        {
            fileName = fileName[(fileName.LastIndexOf(separator) + 1)..];
        }
        if (fileName.StartsWith("mceclip0"))
        {
            fileName = fileName.Replace("mceclip0", Random.Shared.Next(100000, 999999).ToString());
        }
        return fileName.SanitizeFileName();
    }

    private static string GetFileSlug(string fullPath, string fileName, string extension)
    {
        string slug = fileName.ToSlug();
        string slugOriginal = slug;
        for (int i = 1; i < 100; i++)
        {
            if (!File.Exists(Path.Combine(fullPath, $"{slug}{extension}")))
                return slug;
            slug = $"{slugOriginal}-{i}";
        }
        throw new InvalidOperationException("Could not generate a unique file slug.");
    }

    public static AssetType GetAssetType(string fileExtension)
    {
        fileExtension = fileExtension.Replace(".", "");

        return AppConstants.ImageExtensions.Contains(fileExtension)
            ? AssetType.Image
            : AppConstants.VideoExtensions.Contains(fileExtension)
                ? AssetType.Video
                : AppConstants.AudioExtensions.Contains(fileExtension) ? AssetType.Audio : AssetType.Attachment;
    }

}
