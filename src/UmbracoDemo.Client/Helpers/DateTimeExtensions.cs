namespace UmbracoDemo.Client.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset? MinValueAsNull(this DateTimeOffset dateTime)
        {
            return dateTime == DateTimeOffset.MinValue ? null : dateTime;
        }

        public static DateTimeOffset? MinValueAsNull(this DateTimeOffset? dateTime)
        {
            return dateTime == DateTimeOffset.MinValue ? null : dateTime;
        }
    }
}
