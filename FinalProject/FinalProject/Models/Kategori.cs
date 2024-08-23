using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Kategori
{
    public int Id { get; set; }

    public string KategoriAdi { get; set; } = null!;

    public virtual ICollection<Urunler> Urunlers { get; set; } = new List<Urunler>();
}
