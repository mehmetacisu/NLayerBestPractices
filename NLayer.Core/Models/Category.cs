namespace NLayer.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        //navigation prop
        public ICollection<Product> Products { get; set; }
    }
}
