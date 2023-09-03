using System.Text.Json;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.Models;

namespace UmbracoDemo.CMS.Custom;

public class BlogTagsFilter : IFilterHandler, IContentIndexHandler
{
    private const string TagsSpecifier = "tags:";
    private const string FieldName = "tags";

    public bool CanHandle(string query) => query.StartsWith(TagsSpecifier, StringComparison.OrdinalIgnoreCase);

    public FilterOption BuildFilterOption(string filter)
    {
        var fieldValue = filter[TagsSpecifier.Length..];
        var values = fieldValue.Split(',');

        return new FilterOption
        {
            FieldName = FieldName,
            Values = values,
            Operator = FilterOperation.Is
        };
    }

    public IEnumerable<IndexFieldValue> GetFieldValues(IContent content, string? culture)
    {
        var tagsValue = content.GetValue<string>(FieldName, culture);
        if (tagsValue is null)
        {
            return Array.Empty<IndexFieldValue>();
        }

        var tags = JsonSerializer.Deserialize<string[]>(tagsValue) ?? Enumerable.Empty<string>();

        return new[]
        {
            new IndexFieldValue
            {
                FieldName = FieldName,
                Values = tags
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