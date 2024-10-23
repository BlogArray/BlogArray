using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogArray.Shared.Extensions;

public static class StringExtensions
{
    //private static readonly Regex RegexStripHtml = new("<[^>]*>", RegexOptions.Compiled);
    internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public static string ToUniqueSlug(this string title)
    {
        string str = title + GetUniqueKey(5);

        return str.ToSlug();
    }

    public static string ToSlug(this string title)
    {
        string str = title.ToLowerInvariant();
        str = str.Trim('-', '_', ' ');

        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        //Removes Emojies
        str = Regex.Replace(str, @"\p{Cs}", "");
        str = Regex.Replace(str, @"[^\u0000-\u007F]+", "");

        byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(str);

        str = Encoding.UTF8.GetString(bytes);

        str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);

        str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);

        str = RemoveIllegalCharacters(str);

        return str.Trim('-', '_', ' ');
    }

    private static string RemoveIllegalCharacters(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        string[] chars = [
                ":", "/", "?", "!", "#", "[", "]", "{", "}", "@", "*", ".", ",", "—", "―", "‖" ,"‗",
                "\"", "&", "'", "~", "$", "(", ")", "`", "%", "^", "|", ";", "<", ">", "\\", "+",
                "_", "=", "⌘", "…"
            ];

        foreach (string ch in chars)
        {
            text = text.Replace(ch, string.Empty);
        }

        text = text.Replace("–", "-");
        text = text.Replace(" ", "-");

        text = RemoveUnicodePunctuation(text);
        text = RemoveDiacritics(text);
        text = RemoveExtraHyphen(text);

        return System.Web.HttpUtility.HtmlEncode(text).Replace("%", string.Empty);
    }

    private static string RemoveUnicodePunctuation(string text)
    {
        string normalized = text.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new();

        foreach (char c in
            normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) is not UnicodeCategory.InitialQuotePunctuation and
                                  not UnicodeCategory.FinalQuotePunctuation))
        {
            _ = sb.Append(c);
        }

        return sb.ToString();
    }

    private static string RemoveDiacritics(string text)
    {
        string normalized = text.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new();

        foreach (char c in
            normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
        {
            _ = sb.Append(c);
        }

        return sb.ToString();
    }

    private static string RemoveExtraHyphen(string text)
    {
        if (text.Contains("--"))
        {
            text = text.Replace("--", "-");
            return RemoveExtraHyphen(text);
        }

        return text;
    }

    public static string GetUniqueKey(int size)
    {
        byte[] data = new byte[4 * size];
        using (RandomNumberGenerator crypto = RandomNumberGenerator.Create())
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new(size);
        for (int i = 0; i < size; i++)
        {
            uint rnd = BitConverter.ToUInt32(data, i * 4);
            long idx = rnd % chars.Length;

            _ = result.Append(chars[idx]);
        }
        return result.ToString().ToLower();
    }

    public static string SanitizePath(this string str)
    {
        str = str.Replace("%2E", ".").Replace("%2F", "/");

        return str.Contains("..") || str.Contains("//") ? throw new ApplicationException("Invalid directory path") : str;
    }

    public static string SanitizeFileName(this string str)
    {
        str = str.SanitizePath();

        //TODO: add filename specific validation here

        return str;
    }

    //public static AssetType GetAssetType(this string fileExtension)
    //{
    //    fileExtension = fileExtension.Replace(".", "");

    //    return AppConstants.ImageExtensions.Contains(fileExtension)
    //        ? AssetType.Image
    //        : AppConstants.VideoExtensions.Contains(fileExtension)
    //            ? AssetType.Video
    //            : AppConstants.AudioExtensions.Contains(fileExtension) ? AssetType.Audio : AssetType.Attachment;
    //}

    //public static string GetAssetThumbnail(this string filePath, AssetType assetType)
    //{
    //    return assetType.Equals(AssetType.Image)
    //        ? filePath
    //        : assetType.Equals(AssetType.Audio)
    //            ? AppConstants.AudioThumbnail
    //            : assetType.Equals(AssetType.Video) ? AppConstants.VideoThumbnail : AppConstants.OtherThumbnail;
    //}

    public static string ToSize(long bytes, int decimals = 2)
    {
        if (bytes == 0)
        {
            return "0 Bytes";
        }

        const int k = 1024;
        int dm = decimals < 0 ? 0 : decimals;
        string[] sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];

        int i = (int)Math.Floor(Math.Log(bytes) / Math.Log(k));

        double formattedSize = bytes / Math.Pow(k, i);
        string format = $"F{dm}";
        return $"{formattedSize.ToString(format)} {sizes[i]}";
    }
}