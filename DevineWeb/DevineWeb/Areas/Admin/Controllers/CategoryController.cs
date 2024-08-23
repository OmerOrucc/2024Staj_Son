using Devine.DataAccess.Repository.IRepository;
using Devine.Models.Models;
using Devine.Utility;
using DevineWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevineWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is  an invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is  an invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
//using DevineWeb.Data;
//using DevineWeb.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace DevineWeb.Controllers
//{
//    public class CategoryController : Controller
//    {
//        private readonly ApplicationDbContext _db;

//        public CategoryController(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        public IActionResult Index()
//        {
//            List<Category> objCategoryList = _db.Categories.ToList();
//            return View(objCategoryList);
//        }

//        // GET: /Category/Edit/5
//        public IActionResult Edit(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound();
//            }

//            var categoryFromDb = _db.Categories.Find(id);
//            if (categoryFromDb == null)
//            {
//                return NotFound();
//            }

//            return View(categoryFromDb);
//        }

//        // POST: /Category/Edit
//        [HttpPost]
//        public IActionResult Edit(Category obj)
//        {
//            if (obj.Name == obj.DisplayOrder.ToString())
//            {
//                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
//            }
//            if (obj.Name.ToLower() == "test")
//            {
//                ModelState.AddModelError("", "Test is an invalid value.");
//            }
//            if (ModelState.IsValid)
//            {
//                var categoryFromDb = _db.Categories.Find(obj.Id);
//                if (categoryFromDb == null)
//                {
//                    return NotFound();
//                }

//                categoryFromDb.Name = obj.Name;
//                categoryFromDb.DisplayOrder = obj.DisplayOrder;

//                _db.Categories.Update(categoryFromDb);
//                _db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(obj);
//        }

//        // GET: /Category/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: /Category/Create
//        [HttpPost]
//        public IActionResult Create(Category obj)
//        {
//            if (obj.Name == obj.DisplayOrder.ToString())
//            {
//                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
//            }
//            if (obj.Name.ToLower() == "test")
//            {
//                ModelState.AddModelError("", "Test is an invalid value.");
//            }
//            if (ModelState.IsValid)
//            {
//                _db.Categories.Add(obj);
//                _db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(obj);
//        }
//    }
//}
