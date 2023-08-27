using Umbraco.Cms.Core;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.Models;

namespace UmbracoDemo.CMS.Custom;

public class DateSort : ISortHandler, IContentIndexHandler
{
    private const string SortOptionSpecifier = "date:";
    private const string FieldName = "date";

    public bool CanHandle(string query)
        => query.StartsWith(SortOptionSpecifier, StringComparison.OrdinalIgnoreCase);

    public SortOption BuildSortOption(string sort)
    {
        var sortDirection = sort[SortOptionSpecifier.Length..];

        return new SortOption
        {
            FieldName = FieldName,
            Direction = sortDirection.StartsWith("asc", StringComparison.OrdinalIgnoreCase)
                ? Direction.Ascending
                : Direction.Descending
        };
    }

    public IEnumerable<IndexFieldValue> GetFieldValues(IContent content, string? culture)
    {
        var date = content.GetValue<DateTime?>("date") ?? content.PublishDate;
        if (date is null)
        {
            return Enumerable.Empty<IndexFieldValue>();
        }

        return new[]
        {
            new IndexFieldValue
            {
                FieldName = FieldName,
                Values = new object[] { date }
            }
        };
    }

    public IEnumerable<IndexField> GetFields()
        => new[]
        {
            new IndexField
            {
                FieldName = FieldName,
                FieldType = FieldType.Date,
                VariesByCulture = false
            }
        };
}