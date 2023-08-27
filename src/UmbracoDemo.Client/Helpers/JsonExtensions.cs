using System.Text.Json;

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
            if (caseInsensitiveDict.TryGetValue(property.Name, out var value))
            {
                property.SetValue(someObject,
                    property.PropertyType.IsInstanceOfType(value)
                        ? value
                        : value.GetValue(property.PropertyType),
                    null);
            }
        }

        return someObject;
    }
}