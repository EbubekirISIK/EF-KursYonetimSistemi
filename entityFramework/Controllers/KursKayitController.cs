using entityFramework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace entityFramework.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;
        public KursKayitController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
             var kurskayit = await _context.KursKayitlari
                                .Include(x => x.Ogrenci)
                                .Include(x => x.Kurs)
                                .Include(x => x.Ogretmen)
                                .ToListAsync();
            return View(kurskayit);  
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler =new SelectList(await _context.Ogrenci.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(KursKayit model)
        {
            model.KayitTarihi = DateTime.Now; 
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenci.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            //var ogrenci = await _context.Ogrenci.FindAsync(id); // sadece id eşleştirmesi yapabiliriz. findAsync ile Inculde kullanılamaz.
            //var ogrenci = await _context.Ogrenci.FirstOrDefaultAsync(o => o.Id == id);
            //bunda ise istediğimiz her türlü eşleştirmeyi yapabiliriz. bu bize liste vermez, ik bulduğu bilgiyi döndürür.
            var kurskaydi = await _context.KursKayitlari
                            //.Include(x => x.KursKayitlari)
                            //.ThenInclude(x => x.Kurs)
                            .FirstOrDefaultAsync(o => o.KayitId == id);
            if (kurskaydi == null)
            {
                return NotFound();
            }

            return View(kurskaydi);

        }
        [HttpPost]
        //[ValidateAntiForgeryToken] //cross site ataklarını engeller
        public async Task<IActionResult> Edit(int id, KursKayit model)
        {
            if (id != model.KayitId)
            {
                return NotFound();

            }
           
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.KursKayitlari.Any(o => o.KayitId == model.KayitId))
                    {
                        return NotFound();
                    }
                    else throw;
                }
                return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kurskayit = await _context.KursKayitlari.FindAsync(id); //route tan gelen bilgidir.
            if (kurskayit == null)
            {
                return NotFound();
            }
            return View(kurskayit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int KayitId) //burda formdan gelen bilgidir.
        {
            var kurskayit = await _context.KursKayitlari.FindAsync(KayitId);
            if (kurskayit == null)
            {
                return NotFound();
            }

            _context.Remove(kurskayit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
