using UmbracoDemo.Client.Models.DataTypes;
using UmbracoDemo.Client.Models.DataTypes.Abstractions;

namespace UmbracoDemo.Client.Models.Blocks;

public class TextBlock : BaseBlock<TextBlockProperties>
{
}

public class TextBlockProperties
{
    public RichText? Text { get; set; }
}