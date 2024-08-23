using DevineWebRazor_Temp.Data;
using DevineWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevineWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category? Category { get; set; }  // Nullable olarak i�aretlendi

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            // Kategori ba�latma kodu burada olabilir
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();  // Model ge�erli de�ilse formu yeniden y�kle
            }

            _db.Categories.Add(Category!);  // Null check yap�ld�ktan sonra kullan�labilir
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
