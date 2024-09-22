namespace BlogArray.Domain.Constants;

public static class RoleConstants
{
    public const string Admin = "ADMIN";
    public const string Editor = "EDITOR";
    public const string Author = "AUTHOR";
    public const string Subscriber = "SUBSCRIBER";
    public const string All = $"{Admin},{Editor},{Author},{Subscriber}";
    public const string AdminEditorAuthor = $"{Admin},{Editor},{Author}";
    public const string AdminEditor = $"{Admin},{Editor}";
}
