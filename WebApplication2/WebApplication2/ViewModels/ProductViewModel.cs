using System.ComponentModel.DataAnnotations;
namespace WebApplication2.ViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Kategori ID girilmeli.")]
        public int KategoriId { get; set; }

        [Required(ErrorMessage = "Ürün adı girilmeli.")]
        public string UrunAdi { get; set; }

        [Required(ErrorMessage = "Ürün açıklaması girilmeli.")]
        public string UrunAciklama { get; set; }

        [Required(ErrorMessage = "Ürün fiyatı girilmeli.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ürün fiyatı geçerli bir değer olmalı.")]
        public decimal UrunFiyat { get; set; }

        [Required(ErrorMessage = "Ürün stok bilgisi girilmeli.")]
        [Range(0, int.MaxValue, ErrorMessage = "Ürün stok bilgisi geçerli bir değer olmalı.")]
        public int UrunStok { get; set; }

        public string UrunGorsel { get; set; }

        [Required(ErrorMessage = "Ürün durumu seçilmeli.")]
        public string UrunDurum { get; set; }
    }
}
