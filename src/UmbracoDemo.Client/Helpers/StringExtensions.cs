namespace UmbracoDemo.Client.Helpers;

public static class StringExtensions
{
    public static string FirstToLower(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        return char.ToLower(input[0]) + input[1..];
    }

    public static string TrimEnd(this string source, string value)
    {
        return !source.EndsWith(value)
            ? source
            : source.Remove(source.LastIndexOf(value));
    }
}