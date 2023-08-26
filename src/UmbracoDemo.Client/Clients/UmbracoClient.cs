using UmbracoDemo.Client.Helpers;
using UmbracoDemo.Client.Models.Pages.Abstractions;

namespace UmbracoDemo.Client.Clients
{
    public interface IUmbracoClient
    {
        Task<BasePage?> GetContentByPath(string path, bool preview = false);
    }

    internal class UmbracoClient : IUmbracoClient
    {
        private readonly IUmbracoApi _umbracoApi;

        public UmbracoClient(IUmbracoApi umbracoApi)
        {
            _umbracoApi = umbracoApi;
        }

        public async Task<BasePage?> GetContentByPath(string path, bool preview = false)
        {
            try
            {
                var response = await _umbracoApi.GetContentItemByPathAsync(path);
                return response.ConvertToPage(preview);
            }
            catch (ApiException e) when (e.StatusCode == 404)
            {
                return null;
            }
        }
    }
}
