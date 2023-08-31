using UmbracoDemo.Client.Helpers;
using UmbracoDemo.Client.Models.Pages.Abstractions;
using UmbracoDemo.Client.Services;

namespace UmbracoDemo.Client.Clients;

public interface IUmbracoClient
{
    Task<BasePage?> GetContentByPath(string path);

    Task<T?> GetContentSingleByType<T>(string? culture = null,
        CancellationToken cancellationToken = default) where T : class, IContent, new();

    Task<ICollection<BasePage>> GetChildrenByPath(
        string path,
        string[]? filter = null,
        string[]? sort = null,
        CancellationToken cancellationToken = default);

    Task<(ICollection<BasePage> Pages, long Total)> Search(string query, int skip, int take, string? culture = null,
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

    public async Task<BasePage?> GetContentByPath(string path)
    {
        try
        {
            bool preview = await _previewService.GetPreview();
            var response = await _umbracoApi.GetContentItemByPathAsync(path, preview: preview, api_Key: preview ? _apiKey : null);
            return response.ConvertToPage(preview);
        }
        catch (ApiException e) when (e.StatusCode == 404)
        {
            return null;
        }
    }

    public async Task<T?> GetContentSingleByType<T>(string? culture = null, CancellationToken cancellationToken = default) where T : class, IContent, new()
    {
        try
        {
            bool preview = await _previewService.GetPreview();
            var response = await _umbracoApi.GetContentAsync(filter: new[] { $"contentType:{T.ContentTypeAlias}" }, take: 1, accept_Language: culture, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
            return response.Items.FirstOrDefault()?.ToContent<T>(preview);
        }
        catch (ApiException e) when (e.StatusCode == 404)
        {
            return null;
        }
    }

    public async Task<ICollection<BasePage>> GetChildrenByPath(string path, string[]? filter = null, string[]? sort = null, CancellationToken cancellationToken = default)
    {
        bool preview = await _previewService.GetPreview();
        var response = await _umbracoApi.GetContentAsync(fetch: $"children:{path}", filter: filter ?? Enumerable.Empty<string>(), sort: sort ?? new[] { "sortOrder:asc" }, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
        return response.Items
            .Select(i => i.ConvertToPage(preview))
            .OfType<BasePage>()
            .ToList();
    }

    public async Task<(ICollection<BasePage> Pages, long Total)> Search(
        string query,
        int skip,
        int take,
        string? culture = null,
        CancellationToken cancellationToken = default)
    {
        bool preview = await _previewService.GetPreview();
        PagedIApiContentResponseModel response = await _umbracoApi.GetContentAsync(filter: new[] { $"search:{query}", "hideFromSearch:false" }, skip: skip, take: take, accept_Language: culture, preview: preview, api_Key: preview ? _apiKey : null, cancellationToken: cancellationToken);
        return (response.Items
            .Select(i => i.ConvertToPage(preview))
            .OfType<BasePage>()
            .ToList(), response.Total);
    }
}