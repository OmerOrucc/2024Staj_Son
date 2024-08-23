using Newtonsoft.Json;

namespace InternWeb.Models
{
    public class kategori
    {
        public int id { get; set; }
        public string kategoriAdi { get; set; }

        [JsonIgnore]
        public ICollection<Product> urunler { get; set; }
    }
}
