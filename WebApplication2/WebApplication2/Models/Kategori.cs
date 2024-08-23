using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Kategori
{
    public int Id { get; set; }

    public string KategoriAdi { get; set; } = null!;

    public virtual ICollection<Urunler> Urunler { get; set; } = new List<Urunler>();
}
