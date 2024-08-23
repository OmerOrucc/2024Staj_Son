using InternWeb.Models;
using System.Linq;
using System.Collections.Generic;
using InternWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using InternWeb.Extensions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InternWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Urunler()
        {
            var categories = await _context.Kategori.ToListAsync();

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.kategoriAdi
            }).ToList();

            ViewBag.CategoryList = categoryList;

            var products = await _context.Urunler.ToListAsync();

            return View(products);
        }


        public IActionResult Profile()
        {
            return View();
        }

         [HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var product = _context.Urunler.Find(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
            
        //    var model = new ProductViewModel
        //    {
        //        Id = product.id,
        //        KategoriId = product.kategoriId,
        //        UrunAdi = product.urunAdi,
        //        UrunAciklama = product.urunAciklama,
        //        UrunFiyat = product.urunFiyat,
        //        UrunStok = product.urunStok,
        //        UrunDurum = product.urunDurum,
        //        UrunGorsel = product.urunGorsel
        //    };

        //        return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(ProductViewModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var product = new Product
        //        {
        //            kategoriId = model.KategoriId,
        //            urunAdi = model.UrunAdi,
        //            urunAciklama = model.UrunAciklama,
        //            urunFiyat = model.UrunFiyat,
        //            urunStok = model.UrunStok,
        //            urunGorsel = model.UrunGorsel, // base64 formatında görsel verisi
        //            urunDurum = model.UrunDurum,
        //            eklenmeTarihi = DateTime.Now
        //        };

        //        _context.Urunler.Add(product);
        //        await _context.SaveChangesAsync();

        //        TempData["SuccessMessage"] = "Ürün başarıyla eklendi!";
        //        return RedirectToAction("Urunler");
        //    }

        //    TempData["ErrorMessage"] = "Hata! Girilen bilgiler veritabanına işlenemedi.";
        //    return RedirectToAction("Urunler");
        //}
        public IActionResult GetProduct(int id)
        {
            var product = _context.Urunler.Find(id); // Veritabanından ürünü bul
            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                Id = product.id,
                KategoriId = product.kategoriId,
                UrunAdi = product.urunAdi,
                UrunAciklama = product.urunAciklama,
                UrunFiyat = product.urunFiyat,
                UrunStok = product.urunStok,
                UrunDurum = product.urunDurum, // Durumu bool'e çevir
                UrunGorsel = product.urunGorsel
            };

            return Json(productViewModel);

        }

        //public IActionResult Delete(int id)
        //{
        //    var product = _context.Urunler.Find(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new ProductViewModel
        //    {
        //        Id = product.id,
        //        KategoriId = product.kategoriId,
        //        UrunAdi = product.urunAdi,
        //        UrunAciklama = product.urunAciklama,
        //        UrunFiyat = product.urunFiyat,
        //        UrunStok = product.urunStok,
        //        UrunDurum = product.urunDurum,
        //        UrunGorsel = product.urunGorsel
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Delete(ProductViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var product = _context.Urunler.Find(model.Id);
        //        _context.Urunler.Remove(product);
        //        _context.SaveChanges();
        //        //TempData["SuccessMessage"] = "Ürün başarıyla silindi!";
        //        return RedirectToAction("Urunler");
        //    }

        //    //TempData["ErrorMessage"] = "Ürün silinirken bir hata oluştu: ";
        //    return View(model);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _context.Urunler.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Urunler.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
        public async Task<IActionResult> UrunEkleForm(ProductViewModel model)
        {
            // Kategorileri al
            var categories = await _context.Kategori.ToListAsync();

            // Kategori listesini oluştur
            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.kategoriAdi
            }).ToList();

            // ViewBag ile kategori listesini gönder
            ViewBag.CategoryList = categoryList;

            
            // PartialView döndür
            return PartialView("UrunEkleForm", model);
        }

            [HttpPost]
            public async Task<IActionResult> UrunEkle(ProductViewModel model)
            {
                    if (ModelState.IsValid)
                    {
                        var product = new Product
                        {
                            kategoriId = model.KategoriId,
                            urunAdi = model.UrunAdi,
                            urunAciklama = model.UrunAciklama,
                            urunFiyat = model.UrunFiyat,
                            urunStok = model.UrunStok,
                            urunGorsel = model.UrunGorsel, // base64 formatında görsel verisi
                            urunDurum = model.UrunDurum,
                            eklenmeTarihi = DateTime.Now
                        };

                        _context.Urunler.Add(product);
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Ürün başarıyla eklendi!";
                        return RedirectToAction("Urunler");
                    }

                    TempData["ErrorMessage"] = "Hata! Girilen bilgiler veritabanına işlenemedi.";
                    return RedirectToAction("Urunler");
            }
        [HttpPost]
        public IActionResult UrunGuncelle(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Urunler.Find(model.Id); // Veritabanından ürünü bul
                if (product == null)
                {
                    return NotFound();
                }

                product.urunAdi = model.UrunAdi;
                product.urunAciklama = model.UrunAciklama;
                product.urunFiyat = model.UrunFiyat;
                product.urunStok = model.UrunStok;
                product.urunDurum = model.UrunDurum;// Bool değeri "Aktif"/"Pasif"e çevir
                product.urunGorsel = model.UrunGorsel;

                _context.SaveChanges(); // Değişiklikleri kaydet

                return RedirectToAction("Index"); // İşlem başarılı ise ana sayfaya dön
            }

            // Model geçerli değilse formu tekrar göster
            return View(model);
        }

        public IActionResult Activities()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Compose()
        {
            return View();
        }

        public IActionResult Mailbox()
        {
            return View();
        }

        public IActionResult ReadMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPage model)
        {
            var json = JsonSerializer.Serialize(new { kullaniciMail = model.Username, kullaniciSifre = model.Password });
            var isValidUser = true; // Bu kısımda gerçek doğrulama yapılmalı
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44382/api/User/login");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var deg = await response.Content.ReadAsStringAsync();
            if (deg == "Invalid login attempt")
            {
                isValidUser = false;
            }
            if (isValidUser)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username ?? "Anonymous")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Dashboard", "Account");
            }
            else
            {
                TempData["ErrorMessage"] = "Mail ya da şifre yanlış.";
            }

            return View(model);
        }

        public class LoginResponse
        {
            public string Token { get; set; }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                KullaniciMail = model.KullaniciMail,
                KullaniciSifre = model.KullaniciSifre,
                KullaniciAdi = model.KullaniciAdi,
                KullaniciSoyadi = model.KullaniciSoyadi,
                KullaniciAdres = model.KullaniciAdres
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Kayıt işleminiz başarılı bir şekilde tamamlandı!";

            return RedirectToAction("Register", new { success = true });
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }

    public static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }
    }
}


