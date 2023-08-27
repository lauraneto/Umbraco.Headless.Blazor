using UmbracoDemo.Client.Models.DataTypes;
using UmbracoDemo.Client.Models.DataTypes.Abstractions;

namespace UmbracoDemo.Client.Models.Blocks;

public class ImageBlock : BaseBlock<ImageBlockProperties>
{
}

public class ImageBlockProperties
{
    public List<Image>? Image { get; set; }
}