using Microsoft.AspNetCore.Mvc;
using InternWeb.Services;

namespace InternWeb.Controllers
{
    public class UrunController : Controller
    {
        private readonly UrunService _urunService;
        public UrunController(UrunService urunService)
        {
            _urunService = urunService;
        }

        public async Task<IActionResult> Index()
        {
            var urunWithKategori = await _urunService.GetUrunWithKategoriAsync();
            return View();
        }
    }
}
