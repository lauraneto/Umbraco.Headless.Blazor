using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.Models;

namespace UmbracoDemo.CMS.Custom;

public class HideFromSearchFilter : IFilterHandler, IContentIndexHandler
{
    private const string CustomSearchSpecifier = "hideFromSearch:";
    private const string FieldName = "hideFromSearch";

    public bool CanHandle(string query)
        => query.StartsWith(CustomSearchSpecifier, StringComparison.OrdinalIgnoreCase);

    public FilterOption BuildFilterOption(string filter)
    {
        var fieldValue = filter[CustomSearchSpecifier.Length..];

        return new FilterOption
        {
            FieldName = FieldName,
            Values = new[] { fieldValue.ToLowerInvariant() },
            Operator = FilterOperation.Is
        };
    }

    public IEnumerable<IndexFieldValue> GetFieldValues(IContent content, string? culture)
    {
        bool hideFromSearch = true;

        // Ideally we would check if the content has a certain composition, but that does not seem possible (easy) :(
        if (content.ContentType.Alias.StartsWith("page"))
        {
            hideFromSearch = content.GetValue<bool>("hideFromSearch", culture);
        }

        return new[]
        {
            new IndexFieldValue
            {
                FieldName = FieldName,
                Values = new object[] { hideFromSearch ? "true" : "false" }
            }
        };
    }

    public IEnumerable<IndexField> GetFields() => new[]
    {
        new IndexField
        {
            FieldName = FieldName,
            FieldType = FieldType.StringRaw,
            VariesByCulture = true
        }
    };
}