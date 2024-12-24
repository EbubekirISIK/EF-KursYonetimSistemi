using entityFramework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace entityFramework.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenci.ToListAsync();
            return View(ogrenciler);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenci.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();  
            }

            //var ogrenci = await _context.Ogrenci.FindAsync(id); // sadece id eşleştirmesi yapabiliriz. findAsync ile Inculde kullanılamaz.
            //var ogrenci = await _context.Ogrenci.FirstOrDefaultAsync(o => o.Id == id);
            //bunda ise istediğimiz her türlü eşleştirmeyi yapabiliriz. bu bize liste vermez, ik bulduğu bilgiyi döndürür.
            var ogrenci = await _context.Ogrenci
                            .Include(x => x.KursKayitlari)
                            .ThenInclude(x =>x.Kurs)
                            .FirstOrDefaultAsync(o => o.OgrenciId == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);

        }
        [HttpPost]
        [ValidateAntiForgeryToken] //cross site ataklarını engeller
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!_context.Ogrenci.Any(o => o.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }
                    else throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var ogrenci = await _context.Ogrenci.FindAsync(id); //route tan gelen bilgidir.
            if(ogrenci == null)
            {  
                return NotFound(); 
            }
            return View(ogrenci);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm]int OgrenciId) //burda formdan gelen bilgidir.
        {
            var ogrenci = await _context.Ogrenci.FindAsync(OgrenciId);
            if (ogrenci == null)
            {
                return NotFound();
            }

            _context.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
