namespace UserAuthService.Models
{
    public class User
    {
        public int id { get; set; }
        public int kullaniciId { get; set; }
        public string kullaniciMail { get; set; }
        public string kullaniciAdi { get; set; }
        public string kullaniciSoyadi { get; set; }
        public string kullaniciAdres { get; set; }
        public string kullaniciSifre { get; set; }
        public int KullaniciYetki { get; set; }  
    }
}
