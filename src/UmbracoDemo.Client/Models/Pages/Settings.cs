using UmbracoDemo.Client.Models.DataTypes;
using UmbracoDemo.Client.Models.Pages.Abstractions;

namespace UmbracoDemo.Client.Models.Pages
{
    public class Settings : BasePage, IContent
    {
        public static string ContentTypeAlias => "settings__cad";

        public List<Link>? HomeLink { get; set; }

        public List<Link>? HeaderLinks { get; set; }

        public List<Link>? SearchLink { get; set; }
    }
}
