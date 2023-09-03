namespace UmbracoDemo.Client.Umbraco;

public partial class ApiLinkModel
{
    public string GetUrl()
    {
        return Url ?? Route?.GetUrl() ?? "#";
    }
}