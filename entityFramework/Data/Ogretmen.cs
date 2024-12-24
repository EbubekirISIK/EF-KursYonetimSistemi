using System.ComponentModel.DataAnnotations;

namespace entityFramework.Data
{
    public class Ogretmen
    {
        [Key]
        public int OgretmenId { get; set; }
        public string? OgretemenAd { get; set; }
        public string? OgretemenSoyad { get; set; }
        public string AdSoyad
        {
            get
            {
                return this.OgretemenAd + " " + this.OgretemenSoyad;
            }
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}
