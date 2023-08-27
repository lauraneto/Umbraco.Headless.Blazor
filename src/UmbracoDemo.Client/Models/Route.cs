namespace UmbracoDemo.Client.Models;

public class Route
{
    public required string Path { get; set; }

    public required StartItem StartItem { get; set; }

    public string GetUrl()
    {
        return Path.Trim('/');
    }
}