using InternWeb.Models;
using InternWeb;
using InternWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevineWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Urunler.ToList();
            return View(products);
        }
        public IActionResult GetProducts()
        {
            var products = _db.Urunler.ToList();
            return PartialView("_ProductTable", products);
        }

        // Diğer metotlar burada...

        // GET: Admin/Product/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _db.Urunler.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        // POST: Admin/Product/Edit
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Urunler.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Urunler.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: Admin/Product/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _db.Urunler.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var productFromDb = _db.Urunler.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            _db.Urunler.Remove(productFromDb);
            _db.SaveChanges();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
