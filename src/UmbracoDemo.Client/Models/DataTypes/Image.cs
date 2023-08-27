namespace UmbracoDemo.Client.Models.DataTypes;

public class Image
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }
}