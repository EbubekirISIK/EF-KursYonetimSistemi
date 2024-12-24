using Microsoft.EntityFrameworkCore;

namespace entityFramework.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Kurs> Kurslar { get; set; } = null!;
        public DbSet<Ogrenci> Ogrenci { get; set; } = null!;
        public DbSet<Ogretmen> Ogretmenler { get; set; } = null!;
        public DbSet<KursKayit> KursKayitlari { get; set; } = null!;
        
    }
}
