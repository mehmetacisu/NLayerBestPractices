namespace NLayer.Core.Models
{

    //soyut class olarak tanımlıyoruz ki yeni bir nesne oluşturulamasın
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
