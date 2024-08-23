using Devine.DataAccess.Repository.IRepository;
using Devine.Models.Models;
using Devine.Models.ViewModels;
using DevineWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Devine.Utility;
using Microsoft.AspNetCore.Authorization;

namespace DevineWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ApplicationDbContext db, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Product/Upsert
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        // POST: Admin/Product/Upsert
        [HttpPost]
        public async Task<IActionResult> Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string uploadFolder = Path.Combine(wwwRootPath, "images/product");
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadFolder, fileName);

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Eski resmin tam dosya yolunu oluştur
                        string oldImage = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('/').Replace("/", "\\"));

                        // Eski resmin mevcut olup olmadığını kontrol et ve varsa sil
                        if (System.IO.File.Exists(oldImage))
                        {
                            try
                            {
                                System.IO.File.Delete(oldImage);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Eski resmi silerken hata oluştu: {ex.Message}");
                            }
                        }
                    }

                    // Yeni resmi kaydet
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    productVM.Product.ImageUrl = "/images/product/" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                if (productVM.Product.Id == 0)
                {
                    // Create
                    _db.Products.Add(productVM.Product);
                }
                else
                {
                    // Update
                    var productFromDb = _db.Products.Find(productVM.Product.Id);
                    if (productFromDb != null)
                    {
                        productFromDb.Title = productVM.Product.Title;
                        productFromDb.ListPrice = productVM.Product.ListPrice;
                        productFromDb.Seller = productVM.Product.Seller;
                        productFromDb.CategoryId = productVM.Product.CategoryId;
                        productFromDb.ImageUrl = productVM.Product.ImageUrl;
                        // Diğer gerekli güncellemeler
                        _db.Products.Update(productFromDb);
                    }
                }

                _db.SaveChanges();
                TempData["success"] = productVM.Product.Id == 0 ? "Product created successfully" : "Product updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }


        //// GET: Admin/Product/Delete/5
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var productFromDb = _db.Products.Find(id);
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}

        //// POST: Admin/Product/Delete/5
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    var productFromDb = _db.Products.Find(id);
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Products.Remove(productFromDb);
        //    _db.SaveChanges();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction("Index");
        //}

        // GET: Admin/Product/Index
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
            //var products = _db.Products.ToList(); // veya uygun bir sorgu
            //return View(products);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});
        }
        [HttpDelete]
        [Route("Product/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var ProductToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (ProductToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath =
                        Path.Combine(_hostEnvironment.WebRootPath,
                        ProductToBeDeleted.ImageUrl.TrimStart('/').Replace("/", "\\"));
            if (System.IO.File.Exists(oldImagePath))
            {
                try
                {
                    System.IO.File.Delete(oldImagePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eski resmi silerken hata oluştu: {ex.Message}");
                }
            }
            _unitOfWork.Product.Remove(ProductToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}

