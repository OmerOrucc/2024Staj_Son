using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.ViewModels;


namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly DBsahibindenContext _context;
        public ProductController(DBsahibindenContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult UrunlerList()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            var products = _context.Urunlers.ToList();
            return Json(new { data = products });
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Urunlers.Find(id);
            if (product != null)
            {
                _context.Urunlers.Remove(product);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SupportMessage message)
        {
            if (ModelState.IsValid)
            {
                message.CreatedAt = DateTime.UtcNow;
                _context.SupportMessages.Add(message);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = message.Message });
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _context.SupportMessages
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => m.Message)
                .ToListAsync();
            return Json(messages);
        }

        public IActionResult CanliDestek()
        {
            return View();
        }
        public IActionResult UrunlerGridView()
        {
            var products = _context.Urunlers.ToList();
            return PartialView("UrunlerGridView", products);
        }
        public IActionResult UrunlerForm()
        {
            return View();
        }
        public async Task<IActionResult> UrunEkleForm(int id)
        {
            var model = id > 0 ? await _context.Urunlers
                                      .Where(p => p.Id == id)
                                      .Select(p => new ProductViewModel
                                      {
                                          Id = p.Id,
                                          UrunAdi = p.UrunAdi,
                                          UrunAciklama = p.UrunAciklama,
                                          UrunFiyat = p.UrunFiyat,
                                          UrunStok = p.UrunStok,
                                          UrunDurum = p.UrunDurum,
                                          KategoriId = p.KategoriId,
                                          UrunGorsel = p.UrunGorsel
                                      }).FirstOrDefaultAsync() : new ProductViewModel();

            var categories = await _context.Kategoris.ToListAsync();

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.KategoriAdi
            }).ToList();

            ViewBag.CategoryList = categoryList;

            return PartialView("UrunEkleForm", model);
        }


        public IActionResult Create()
        {
            return View();
        }
    }
}
