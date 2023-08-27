using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Umbraco.Cms.Api.Delivery.Json;
using Umbraco.Cms.Core.Models.DeliveryApi;

namespace UmbracoDemo.CMS.Custom.Swagger;

public class CustomDeliveryApiJsonTypeResolver : DeliveryApiJsonTypeResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = base.GetTypeInfo(type, options);

        // HACK: This is a workaround for the fact that using System.Text.Json the type discriminator needs to be the first property!
        // https://github.com/dotnet/runtime/issues/72604
        if (typeof(IApiElement).IsAssignableFrom(typeInfo.Type)
            && typeInfo.Properties.FirstOrDefault(p => p.Name.InvariantEquals(nameof(IApiElement.ContentType))) is { } contentTypeProperty)
        {
            contentTypeProperty.Order = int.MinValue;
        }

        return typeInfo;
    }
}