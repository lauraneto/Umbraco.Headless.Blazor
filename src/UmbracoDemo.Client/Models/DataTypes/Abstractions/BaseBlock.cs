using System.Text.Json.Serialization;
using UmbracoDemo.Client.Models.Blocks;

namespace UmbracoDemo.Client.Models.DataTypes.Abstractions
{
    public abstract class BaseBlock<T> : BaseBlock
    {
        public T Properties { get; set; }
    }

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "contentType")]
    [JsonDerivedType(typeof(TextBlock), typeDiscriminator: "textBlock__cad")]
    [JsonDerivedType(typeof(ImageBlock), typeDiscriminator: "imageBlock__cad")]
    public class BaseBlock
    {
        public Guid Id { get; set; }
    }
}
