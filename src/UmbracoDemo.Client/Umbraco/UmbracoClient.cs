using UmbracoDemo.Client.Helpers;
using UmbracoDemo.Client.Services;

namespace UmbracoDemo.Client.Umbraco;

public interface IUmbracoClient
{
    Task<IApiContentResponseModel?> GetContentByPath(string path);

    Task<T?> GetContentSingleByType<T>(
        string? culture = null,
        CancellationToken cancellationToken = default)
        where T : IApiContentResponseModel;

    Task<ICollection<IApiContentResponseModel>> GetChildrenByPath(
        string path,
        string[]? filter = null,
        string[]? sort = null,
        CancellationToken cancellationToken = default);

    Task<(ICollection<IApiContentResponseModel> Pages, long Total)> Search(
        string query,
        int skip,
        int take,
        string? culture = null,
        CancellationToken cancellationToken = default);
}

internal class UmbracoClient : IUmbracoClient
{
    private readonly IUmbracoApi _umbracoApi;
    private readonly IPreviewService _previewService;
    private readonly string? _apiKey;

    public UmbracoClient(
        IUmbracoApi umbracoApi,
        IPreviewService previewService,
        IConfiguration config)
    {
        _umbracoApi = umbracoApi;
        _previewService = previewService;
        _apiKey = config["UmbracoApi:ApiKey"];
    }

    public async Task<IApiContentResponseModel?> GetContentByPath(string path)
    {
        try
        {
            bool preview = await _previewService.GetPreview();
            var response = await _umbracoApi.GetContentItemByPathAsync(path, expand: "property:relatedBlogs", preview: preview, api_Key: preview ? _apiKey : null);
            return response;
        }
        catch (ApiException e) when (e.StatusCode == 404)
        {
            return null;
        }
    }

    public async Task<T?> GetContentSingleByType<T>(string? culture = null, CancellationToken cancellationToken = default) where T : IApiContentResponseModel
    {
        try
        {
            bool preview = await _previewService.GetPreview();
            var response = await _umbracoApi.GetContentAsync(filter: new[] { $"contentType:{typeof(T).GetContentTypeAlias()}" }, take: 1, accept_Language: culture, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
            return response.Items.FirstOrDefault() as T;
        }
        catch (ApiException e) when (e.StatusCode == 404)
        {
            return null;
        }
    }

    public async Task<ICollection<IApiContentResponseModel>> GetChildrenByPath(string path, string[]? filter = null, string[]? sort = null, CancellationToken cancellationToken = default)
    {
        bool preview = await _previewService.GetPreview();
        var response = await _umbracoApi.GetContentAsync(fetch: $"children:{path}", filter: filter ?? Enumerable.Empty<string>(), sort: sort ?? new[] { "sortOrder:asc" }, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
        return response.Items;
    }

    public async Task<(ICollection<IApiContentResponseModel> Pages, long Total)> Search(
        string query,
        int skip,
        int take,
        string? culture = null,
        CancellationToken cancellationToken = default)
    {
        bool preview = await _previewService.GetPreview();
        PagedIApiContentResponseModel response = await _umbracoApi.GetContentAsync(filter: new[] { $"search:{query}", "hideFromSearch:false" }, skip: skip, take: take, accept_Language: culture, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
        return (response.Items, response.Total);
    }
}