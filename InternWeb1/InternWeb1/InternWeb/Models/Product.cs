using Newtonsoft.Json;

namespace InternWeb.Models
{
    public class Product
    {
        public int id { get; set; }
        public int kategoriId { get; set; }
        public string urunAdi { get; set; }
        public string urunAciklama { get; set; }
        public decimal urunFiyat { get; set; }
        public int urunStok { get; set; }
        public DateTime eklenmeTarihi { get; set; }
        public string urunDurum { get; set; }
        public string urunGorsel { get; set; }

        [JsonIgnore]
        public kategori Kategori { get; set; }
    }
}
