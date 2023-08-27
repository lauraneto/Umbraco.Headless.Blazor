namespace UmbracoDemo.Client.Models.DataTypes;

public class Link
{
    public string? Url { get; set; }

    public string Title { get; set; }

    public string? Target { get; set; }

    public Guid? DestinationId { get; set; }

    public string? DestinationType { get; set; }

    public Route? Route { get; set; }

    public string LinkType { get; set; }

    public string GetUrl()
    {
        return Url ?? Route?.GetUrl() ?? "#";
    }
}