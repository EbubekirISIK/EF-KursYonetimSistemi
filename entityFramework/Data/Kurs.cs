using System.ComponentModel.DataAnnotations;

namespace entityFramework.Data
{
    public class Kurs
    {
        [Key]
        public int KursId { get; set; }
        public string? Baslik { get; set; }
        public ICollection<KursKayit> kursKayitlari { get; set; } = new List<KursKayit>();
    }
}
