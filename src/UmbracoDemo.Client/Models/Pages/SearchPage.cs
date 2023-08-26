using UmbracoDemo.Client.Models.DataTypes;
using UmbracoDemo.Client.Models.Pages.Abstractions;
using UmbracoDemo.Client.Models.Pages.Compositions;

namespace UmbracoDemo.Client.Models.Pages
{
    public class SearchPage : BasePage, IContent, ICompositionBasePage
    {
        public static string ContentTypeAlias => "searchPage__cad";

        public string? Title { get; set; }

        public RichText? Intro { get; set; }
    }
}
