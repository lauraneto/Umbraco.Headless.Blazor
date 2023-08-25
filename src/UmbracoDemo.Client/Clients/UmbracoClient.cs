namespace UmbracoDemo.Client.Clients
{
    public interface IUmbracoClient
    {
        Task<IApiContentResponseModel?> GetPageByPath(string path);
    }

    internal class UmbracoClient : IUmbracoClient
    {
        private readonly IUmbracoApi _umbracoApi;

        public UmbracoClient(IUmbracoApi umbracoApi)
        {
            _umbracoApi = umbracoApi;
        }

        public async Task<IApiContentResponseModel?> GetPageByPath(string path)
        {
            try
            {
                return await _umbracoApi.GetContentItemByPathAsync(path);
            }
            catch (ApiException e) when (e.StatusCode == 404)
            {
                return null;
            }
        }
    }
}
