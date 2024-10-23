using System.Text.RegularExpressions;

namespace BlogArray.Shared.Helpers;

public static class ReadTimeCalculator
{
    /// <summary>
    /// Calculates the estimated reading time of the given HTML content.
    /// </summary>
    /// <param name="htmlContent">The HTML content to analyze.</param>
    /// <param name="wordsPerMinute">The average reading speed for non-CJK text (default is 200).</param>
    /// <param name="cjkCharactersPerMinute">The average reading speed for CJK characters (default is 500).</param>
    /// <returns>The estimated reading time in minutes, with a minimum of 1 minute.</returns>
    public static int CalculateReadingTime(string htmlContent, int wordsPerMinute = 200, int cjkCharactersPerMinute = 500)
    {
        // 1. Remove HTML tags (except image tags)
        string plainText = StripHtmlTags(htmlContent);

        // 2. Detect if the content contains CJK characters
        bool isCjkContent = ContainsCjkCharacters(plainText);

        // 3. Calculate time in seconds for text (words or characters)
        int textTimeInSeconds = isCjkContent
            ? CalculateCjkReadingTimeInSeconds(plainText, cjkCharactersPerMinute)
            : CalculateNonCjkReadingTimeInSeconds(plainText, wordsPerMinute);

        // 4. Count the number of images
        int imageCount = CountImages(htmlContent);

        // 5. Calculate additional time for images in seconds
        int imageTimeInSeconds = CalculateImageReadTimeInSeconds(imageCount);

        // 6. Total time in seconds (text + images)
        int totalTimeInSeconds = textTimeInSeconds + imageTimeInSeconds;

        // 7. Convert seconds to minutes, rounding up, ensuring minimum of 1 minute
        int totalReadTimeInMinutes = (int)Math.Ceiling(totalTimeInSeconds / 60.0);
        return Math.Max(totalReadTimeInMinutes, 1); // Ensure a minimum of 1 minute
    }

    private static string StripHtmlTags(string html)
    {
        // Remove all tags except <img> tags to leave image-related tags for separate counting
        return Regex.Replace(html, "<(?!img\\b)[^>]*>", string.Empty);
    }

    private static int CountWords(string text)
    {
        // Split the text by whitespace and count non-empty entries
        string[] words = Regex.Split(text, @"\W+");
        return words.Length;
    }

    private static int CountCjkCharacters(string text)
    {
        // Match CJK characters using Unicode range for CJK (Chinese, Japanese, Korean)
        return Regex.Matches(text, @"[\p{IsCJKUnifiedIdeographs}\p{IsHangulSyllables}\p{IsHiragana}\p{IsKatakana}]").Count;
    }

    private static bool ContainsCjkCharacters(string text)
    {
        // Check if the content contains any CJK characters
        return CountCjkCharacters(text) > 0;
    }

    private static int CalculateNonCjkReadingTimeInSeconds(string text, int wordsPerMinute)
    {
        // Calculate reading time in seconds based on word count for non-CJK content
        int wordCount = CountWords(text);
        return (int)Math.Ceiling((double)wordCount / wordsPerMinute * 60);
    }

    private static int CalculateCjkReadingTimeInSeconds(string text, int charactersPerMinute)
    {
        // Calculate reading time in seconds based on character count for CJK content
        int characterCount = CountCjkCharacters(text);
        return (int)Math.Ceiling((double)characterCount / charactersPerMinute * 60);
    }

    private static int CountImages(string html)
    {
        // Count <img> tags in the HTML content
        return Regex.Matches(html, "<img\\b[^>]*>").Count;
    }

    private static int CalculateImageReadTimeInSeconds(int imageCount)
    {
        if (imageCount == 0)
        {
            return 0; // No additional time if there are no images
        }

        // 12 seconds for the first image, 3 seconds for each additional image
        const int firstImageTime = 12;
        const int additionalImageTime = 3;

        if (imageCount == 1)
        {
            return firstImageTime;
        }

        // First image gets 12 seconds, subsequent images get 3 seconds each
        int totalImageTimeInSeconds = firstImageTime + ((imageCount - 1) * additionalImageTime);

        return totalImageTimeInSeconds;
    }
}
