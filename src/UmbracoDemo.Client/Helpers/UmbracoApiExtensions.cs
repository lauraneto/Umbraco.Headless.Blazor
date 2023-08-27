using System.Collections.Concurrent;
using System.Reflection;
using UmbracoDemo.Client.Models;
using UmbracoDemo.Client.Models.Pages.Abstractions;

namespace UmbracoDemo.Client.Helpers;

public static class UmbracoApiExtensions
{
    public static BasePage? ConvertToPage(this IApiContentResponseModel apiContentResponseModel, bool preview)
    {
        return GetPageBuilder(apiContentResponseModel.ContentType)?.Invoke(null, new object?[] { apiContentResponseModel, preview }) as BasePage;
    }

    public static T ToContent<T>(this IApiContentResponseModel source, bool preview) where T : class, IContent, new()
    {
        T content = source.Properties.ToObject<T>() ?? new T();
        if (content is BasePage page)
        {
            page.Id = source.Id;
            page.ContentType = source.ContentType;
            page.Name = source.Name;
            page.Culture = source.Cultures.FirstOrDefault(c => c.Value.Path == source.Route.Path).Key;
            page.Cultures = source.Cultures.ToDictionary(c => c.Key, c => new Route
            {
                Path = c.Value.Path,
                StartItem = new StartItem
                {
                    Id = c.Value.StartItem.Id,
                    Path = c.Value.StartItem.Path
                }
            });
            page.Route = new Route
            {
                Path = source.Route.Path,
                StartItem = new StartItem
                {
                    Id = source.Route.StartItem.Id,
                    Path = source.Route.StartItem.Path
                }
            };
            page.Preview = preview;
        }

        return content;
    }

    public static readonly ConcurrentDictionary<string, MethodInfo?> PageBuilders = new();
    private static MethodInfo? GetPageBuilder(string contentTypeAlias) =>
        PageBuilders.GetOrAdd(contentTypeAlias, key =>
        {
            var basePageType = typeof(BasePage);
            var contentType = basePageType.Assembly.GetTypes().FirstOrDefault(t => basePageType.IsAssignableFrom(t) && typeof(IContent).IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null && t.GetProperty(nameof(IContent.ContentTypeAlias), BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as string == key);
            return contentType == null
                ? null
                : typeof(UmbracoApiExtensions).GetMethod(nameof(ToContent))?.MakeGenericMethod(contentType);
        });
}