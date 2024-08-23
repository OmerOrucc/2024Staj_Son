using InternWeb.Models;
using InternWeb.Repositories;

namespace InternWeb.Services
{
    public class UrunService
    {
        private readonly UrunRepository _urunRepository;
        public UrunService(UrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public async Task<List<KategoriUrunDto>> GetUrunWithKategoriAsync()
        {
            return await _urunRepository.GetUrunWithKategoriAsync();
        }
    }
}
