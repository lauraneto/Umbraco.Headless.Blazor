using UmbracoDemo.Client.Models.DataTypes;

namespace UmbracoDemo.Client.Models.Pages.Compositions
{
    public interface ICompositionBasePage
    {
        public string? Title { get; set; }

        public RichText? Intro { get; set; }
    }
}
