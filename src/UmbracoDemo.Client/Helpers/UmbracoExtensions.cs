namespace UmbracoDemo.Client.Helpers
{
    public static class UmbracoExtensions
    {
        private const string ContentTypeModelSuffix = "ContentResponseModel";
        private const string BlockContentTypeModelSuffix = "ElementModel";
        private const string ContentTypeAliasSuffix = "__cad";

        public static string GetContentTypeAlias(this Type type)
        {
            var name = type.Name
                .TrimEnd(ContentTypeModelSuffix)
                .FirstToLower();

            return name;
        }

        public static string GetContentTypeName(this Type type)
        {
            var name = type.Name
                .TrimEnd(ContentTypeModelSuffix)
                .TrimEnd(ContentTypeAliasSuffix);

            return name;
        }

        public static string GetBlockContentTypeName(this Type type)
        {
            var name = type.Name
                .TrimEnd(BlockContentTypeModelSuffix)
                .TrimEnd(ContentTypeAliasSuffix);

            return name;
        }
    }
}
