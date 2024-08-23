using System.ComponentModel.DataAnnotations;

namespace InternWeb.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email girilmeli.")]
        [EmailAddress(ErrorMessage = "Geçersiz Mail.")]
        public string KullaniciMail { get; set; } // `KullaniciEmail` -> `KullaniciMail`

        [Required(ErrorMessage = "Şifre gerekli.")]
        [DataType(DataType.Password)]
        public string KullaniciSifre { get; set; } // `Password` -> `KullaniciSifre`

        [Required(ErrorMessage = "İsim girilmeli.")]
        public string KullaniciAdi { get; set; } // `Username` -> `KullaniciAdi`

        [Required(ErrorMessage = "Soyisim girilmeli.")]
        public string KullaniciSoyadi { get; set; }

        [Required(ErrorMessage = "Adres girilmeli.")]
        public string KullaniciAdres { get; set; }

        [Required(ErrorMessage = "Şifreyi tekrar girin.")]
        [DataType(DataType.Password)]
        [Compare("KullaniciSifre", ErrorMessage = "Girilen şifreler birbiriyle eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Sözleşmeyi onaylayın.")]
        public bool Terms { get; set; }
    }
}

