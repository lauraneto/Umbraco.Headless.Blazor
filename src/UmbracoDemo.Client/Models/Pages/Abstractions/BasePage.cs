namespace UmbracoDemo.Client.Models.Pages.Abstractions;

public abstract class BasePage
{
    public string ContentType { get; set; } = "";

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Culture { get; set; }

    public bool Preview { get; set; }

    public Dictionary<string, Route> Cultures { get; set; } = new();

    public Route Route { get; set; }
}