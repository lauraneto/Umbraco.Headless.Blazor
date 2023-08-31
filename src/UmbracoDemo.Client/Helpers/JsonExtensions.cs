using System.Text.Json;
using UmbracoDemo.Client.Models.Pages.Abstractions;

namespace UmbracoDemo.Client.Helpers;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public static T? GetValue<T>(this object obj)
    {
        return obj is JsonElement elem ? elem.Deserialize<T>(JsonSerializerOptions) : default;
    }

    public static object? GetValue(this object obj, Type type)
    {
        return obj is JsonElement elem ? elem.Deserialize(type, JsonSerializerOptions) : null;
    }

    public static T? ToObject<T>(this IDictionary<string, object>? source)
        where T : class, new()
    {
        if (source == null)
        {
            return null;
        }

        var caseInsensitiveDict = new Dictionary<string, object>(source, StringComparer.OrdinalIgnoreCase);
        var someObject = new T();

        foreach (var property in typeof(T).GetProperties())
        {
            if (!caseInsensitiveDict.TryGetValue(property.Name, out var value)) continue;

            object? finalValue;
            if (property.PropertyType.IsInstanceOfType(value))
            {
                finalValue = value;
            }
            else if (property.PropertyType.IsGenericType
                     && property.PropertyType.GetGenericArguments().FirstOrDefault() is { } genericArgument
                     && genericArgument.IsAssignableTo(typeof(IContent))
                     && property.PropertyType.IsAssignableTo(typeof(ICollection<>).MakeGenericType(genericArgument)))

            {
                var contentList =
                    value.GetValue(typeof(List<IApiContentResponseModel>)) as List<IApiContentResponseModel>;

                dynamic dynamicValue = Activator.CreateInstance(property.PropertyType);

                if (contentList is { Count: > 0 })
                {
                    foreach (var content in contentList)
                    {
                        dynamicValue.Add((dynamic)content.ConvertToPage());
                    }
                }

                finalValue = dynamicValue;
            }
            else
            {
                finalValue = value.GetValue(property.PropertyType);
            }
            property.SetValue(someObject,
                finalValue,
                null);
        }

        return someObject;
    }
}