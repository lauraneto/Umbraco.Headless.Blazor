using Blazored.LocalStorage;

namespace UmbracoDemo.Client.Services;

public interface IPreviewService
{
    Task UpdatePreview(bool newValue);

    ValueTask<bool> GetPreview();
}

public class PreviewService : IPreviewService
{
    private const string PreviewKey = "Preview";
    private readonly ILocalStorageService _localStorage;
    private bool? _preview;

    public PreviewService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task UpdatePreview(bool newValue)
    {
        _preview = newValue;
        await _localStorage.SetItemAsync(PreviewKey, newValue);
    }

    public async ValueTask<bool> GetPreview()
    {
        return _preview ??= await _localStorage.GetItemAsync<bool>(PreviewKey);
    }
}