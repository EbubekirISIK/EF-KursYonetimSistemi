using entityFramework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace entityFramework.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DataContext _context;
        public OgretmenController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ogretmenler = await _context.Ogretmenler.ToListAsync();
            return View(ogretmenler);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            if (ModelState.IsValid)
            {
                var ogretmen = _context.Ogretmenler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(x => x.OgretmenId == id);
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Ogretmen model)
        {
            if (model.OgretmenId == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
           
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogretmen = await _context.Ogretmenler.FindAsync(id); //route tan gelen bilgidir.
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int OgretmenId) //burda formdan gelen bilgidir. ogretmen sınıfında yazan aynen buraya yazılmalıdır.
        {
            var ogretmen = await _context.Ogretmenler.FindAsync(OgretmenId);
            if (ogretmen == null)
            {
                return NotFound();
            }

            _context.Remove(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
