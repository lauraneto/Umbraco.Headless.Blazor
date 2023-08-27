using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.Models;

namespace UmbracoDemo.CMS.Custom;

public class SearchFilter : IFilterHandler, IContentIndexHandler
{
    private const string CustomSearchSpecifier = "search:";
    private const string FieldName = "search";

    public bool CanHandle(string query)
        => query.StartsWith(CustomSearchSpecifier, StringComparison.OrdinalIgnoreCase);

    public FilterOption BuildFilterOption(string filter)
    {
        var fieldValue = filter.Substring(CustomSearchSpecifier.Length);
        var values = fieldValue.Split(',');

        return new FilterOption
        {
            FieldName = FieldName,
            Values = values,
            Operator = FilterOperation.Contains
        };
    }

    public IEnumerable<IndexFieldValue> GetFieldValues(IContent content, string? culture)
    {
        string? title = content.GetValue<string>("title", culture) ?? content.Name;
        var intro = content.GetValue<string>("intro", culture)?.StripHtml();

        if (title.IsNullOrWhiteSpace() && intro is null)
        {
            return Array.Empty<IndexFieldValue>();
        }

        return new[]
        {
            new IndexFieldValue
            {
                FieldName = FieldName,
                Values = new object?[] { title, intro }.WhereNotNull()
            }
        };
    }

    public IEnumerable<IndexField> GetFields() => new[]
    {
        new IndexField
        {
            FieldName = FieldName,
            FieldType = FieldType.StringAnalyzed,
            VariesByCulture = true
        }
    };
}