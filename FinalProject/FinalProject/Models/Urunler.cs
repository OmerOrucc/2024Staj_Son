using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Urunler
{
    public int Id { get; set; }

    public int KategoriId { get; set; }

    public string UrunAdi { get; set; } = null!;

    public string UrunAciklama { get; set; } = null!;

    public decimal UrunFiyat { get; set; }

    public int UrunStok { get; set; }

    public DateTime EklenmeTarihi { get; set; }

    public string UrunDurum { get; set; } = null!;

    public string UrunGorsel { get; set; } = null!;

    public virtual Kategori Kategori { get; set; } = null!;
}
