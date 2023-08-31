using UmbracoDemo.Client.Models.DataTypes;
using UmbracoDemo.Client.Models.Pages.Abstractions;
using UmbracoDemo.Client.Models.Pages.Compositions;

namespace UmbracoDemo.Client.Models.Pages;

public class BlogDetail : BasePage, IContent, ICompositionBasePage
{
    public static string ContentTypeAlias => "pageBlogDetail__cad";

    public string? Title { get; set; }

    public RichText? Intro { get; set; }

    public List<Image>? Image { get; set; }

    public DateTime? Date { get; set; }

    public BlockList Blocks { get; set; }

    public List<string>? Tags { get; set; }
}