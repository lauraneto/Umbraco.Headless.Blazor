namespace UmbracoDemo.Client.Umbraco;

public interface IPageComposition<T> : IPageComposition where T : PageComposition__cadPropertiesModel
{
    public new T Properties { get; set; }

    PageComposition__cadPropertiesModel IPageComposition.Properties => Properties;
}

public interface IPageComposition
{
    public PageComposition__cadPropertiesModel Properties { get; }
}

public partial class PageHome__cadContentResponseModel : IPageComposition<PageHome__cadPropertiesModel> { }

public partial class PageContent__cadContentResponseModel : IPageComposition<PageContent__cadPropertiesModel> { }

public partial class PageBlogDetail__cadContentResponseModel : IPageComposition<PageBlogDetail__cadPropertiesModel> { }

public partial class PageBlogOverview__cadContentResponseModel : IPageComposition<PageBlogOverview__cadPropertiesModel> { }

public partial class PageSearch__cadContentResponseModel : IPageComposition<PageSearch__cadPropertiesModel> { }