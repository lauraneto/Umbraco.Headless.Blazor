using System.Text.Json.Serialization;

namespace UmbracoDemo.Client.Umbraco;

public partial class IApiContentResponseModel
{
    [JsonPropertyName("contentType")]
    public required string ContentType { get; set; }

    public string? Culture => Enumerable.FirstOrDefault<KeyValuePair<string, IApiContentRouteModel>>(Cultures, c => c.Value.Path == Route.Path).Key;
}