namespace UmbracoDemo.Client.Models.Pages.Abstractions;

public abstract class BasePage
{
    public string ContentType { get; set; } = "";

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Culture { get; set; }

    public bool Preview { get; set; }

    public Dictionary<string, string> Cultures { get; set; } = new();

    public Guid StartItem { get; set; }

    public string Path { get; set; }
}