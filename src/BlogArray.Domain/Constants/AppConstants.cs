using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Constants;

public static class AppConstants
{
    public static string Version { get; set; }

    public static string WebRootPath { get; set; }

    public static string ContentRootPath { get; set; }

    public static string AudioThumbnail = "/content/system/media/music.png";

    public static string VideoThumbnail = "/content/system/media/video-file.png";

    public static string OtherThumbnail = "/content/system/media/attachment.png";

    public static List<string> ImageExtensions = ["png", "gif", "jpeg", "jpg", "webp"];

    public static List<string> AudioExtensions = ["mp3"];

    public static List<string> VideoExtensions = ["mp4", "avi"];

    public static List<string> OtherExtensions = ["zip", "7z", "pdf", "doc", "docx", "xls", "xlsx"];

    public static List<string> FileExtensions = [.. ImageExtensions, .. AudioExtensions, .. VideoExtensions, .. OtherExtensions];
    
}
