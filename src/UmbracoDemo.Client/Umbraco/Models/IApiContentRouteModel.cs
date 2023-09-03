namespace UmbracoDemo.Client.Umbraco;

public partial class IApiContentRouteModel
{
    public string GetUrl()
    {
        return Path.Trim('/');
    }
}