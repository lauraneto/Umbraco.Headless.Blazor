using UmbracoDemo.Client.Models.DataTypes.Abstractions;

namespace UmbracoDemo.Client.Models.DataTypes;

public class BlockList
{
    public List<BlockListItem> Items { get; set; }
}

public class BlockListItem
{
    public BaseBlock? Content { get; set; }

    public BaseBlock? Settings { get; set; }
}