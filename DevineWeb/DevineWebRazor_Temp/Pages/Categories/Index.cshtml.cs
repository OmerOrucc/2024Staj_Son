using DevineWebRazor_Temp.Data;
using DevineWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevineWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
