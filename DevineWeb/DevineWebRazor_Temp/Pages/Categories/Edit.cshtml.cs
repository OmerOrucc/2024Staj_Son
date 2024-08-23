using DevineWebRazor_Temp.Data;
using DevineWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevineWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category? Category { get; set; }  // Nullable olarak iþaretlendi

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Category != null)  // Nullable kontrolü eklenmeli
                {
                    _db.Categories.Update(Category);
                    _db.SaveChanges();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToPage("Index");  // RedirectToAction yerine RedirectToPage kullanmalýsýnýz
                }
                return NotFound();
            }
            return Page();
        }
    }
}
