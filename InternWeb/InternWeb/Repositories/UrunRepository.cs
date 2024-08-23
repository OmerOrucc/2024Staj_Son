using InternWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace InternWeb.Repositories
{
    public class UrunRepository
    {
        private readonly AppDbContext _context;
        public UrunRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<KategoriUrunDto>> GetUrunWithKategoriAsync()
        {
            var query = await (from Product in _context.Urunler
                               join kategori in _context.Kategori on Product.id equals kategori.id into joinedData
                               from kategori in joinedData.DefaultIfEmpty()
                               select new KategoriUrunDto()
                               {
                                   KategoriName = kategori.kategoriAdi,
                                   UrunAdi = Product.urunAdi,
                                   UrunFiyat = Product.urunFiyat,
                                   UrunStok = Product.urunStok,

                              }).ToListAsync();
            return query;
        }
    }
}
