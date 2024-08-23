using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
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
            var products = _context.Urunler.ToList();
            return Json(new { data = products });
        }

        public IActionResult UrunlerGridView()
        {
            var products = _context.Urunler.ToList();
            return PartialView("UrunlerGridView", products);
        }
        public IActionResult UrunlerForm()
        {
            return View();
        }
        public async Task<IActionResult> UrunEkleForm(ProductViewModel model)
        {
            // Kategorileri al
            var categories = await _context.Kategori.ToListAsync();

            // Kategori listesini oluştur
            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.KategoriAdi
            }).ToList();

            // ViewBag ile kategori listesini gönder
            ViewBag.CategoryList = categoryList;


            // PartialView döndür
            return PartialView("UrunEkleForm", model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
