using System.ComponentModel.DataAnnotations;
using System.Data;

namespace entityFramework.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }
        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!;
        public int KursId { get; set; }
        public Kurs Kurs { get; set; } = null!;
        public int? OgretmenId { get; set; } // burda ki soru işaretinin anlamı bu boş olabilir. projeyi geliştirirken kursların öğretmeni yoktu şuan bu özelliği getirdik. bunu tüm kursları doldurduktan snponra düzeltebilirz.
        public Ogretmen Ogretmen { get; set; } = null!;
        public DateTime KayitTarihi { get; set; }

    }
}
