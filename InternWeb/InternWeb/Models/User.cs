using System.ComponentModel.DataAnnotations;

namespace InternWeb.Models
{
    public class User
    {
        public int Id { get; set; } // Veritabanında 'id' sütunu

        [Required]
        public int KullaniciId { get; set; } // Veritabanında 'KullaniciId' sütunu

        [Required]
        [EmailAddress]
        public string KullaniciMail { get; set; } // Veritabanında 'KullaniciMail' sütunu

        [Required]
        public string KullaniciAdi { get; set; } // Veritabanında 'KullaniciAdi' sütunu

        [Required]
        public string KullaniciSoyadi { get; set; } // Veritabanında 'KullaniciSoyadi' sütunu

        public string? KullaniciAdres { get; set; } // Veritabanında 'KullaniciAdres' sütunu

        [Required]
        public string KullaniciSifre { get; set; } // Veritabanında 'KullaniciSifre' sütunu

        [Required]
        public int KullaniciYetki { get; set; } // Veritabanında 'KullaniciYetki' sütunu
    }
}

