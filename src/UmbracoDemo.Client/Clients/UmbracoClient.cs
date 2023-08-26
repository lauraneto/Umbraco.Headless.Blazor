using UmbracoDemo.Client.Helpers;
using UmbracoDemo.Client.Models.Pages.Abstractions;

namespace UmbracoDemo.Client.Clients
{
    public interface IUmbracoClient
    {
        Task<BasePage?> GetContentByPath(string path, bool preview = false);

        Task<T?> GetContentSingleByType<T>(string? culture = null, bool preview = false,
            CancellationToken cancellationToken = default) where T : class, IContent, new();
    }

    internal class UmbracoClient : IUmbracoClient
    {
        private readonly IUmbracoApi _umbracoApi;
        private readonly string? _apiKey;

        public UmbracoClient(IUmbracoApi umbracoApi, IConfiguration config)
        {
            _umbracoApi = umbracoApi;
            _apiKey = config["UmbracoApi:ApiKey"];
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

        public async Task<T?> GetContentSingleByType<T>(string? culture = null, bool preview = false, CancellationToken cancellationToken = default) where T : class, IContent, new()
        {
            try
            {
                var response = await _umbracoApi.GetContentAsync(filter: new[] { $"contentType:{T.ContentTypeAlias}" }, take: 1, accept_Language: culture, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
                return response.Items.FirstOrDefault()?.ToContent<T>(preview);
            }
            catch (ApiException e) when (e.StatusCode == 404)
            {
                return null;
            }
        }
    }
}
