namespace ComicChecklist.Presentation.UI.Enums
{
    public enum IssuesStatusOptions
    {
        Undefined, Pending, InProgress, Completed
    }

    public class IssuesStatusOptionsValues
    {
        public static IssuesStatusOptions[] Values { get; } = (IssuesStatusOptions[])Enum.GetValues(typeof(IssuesStatusOptions));
    }
}
