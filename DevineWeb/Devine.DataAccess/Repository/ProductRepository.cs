using Devine.DataAccess.Repository.IRepository;
using Devine.Models.Models;
using DevineWeb.Data;

namespace Devine.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Product? Find(int? id)
        {
            return _db.Products.Find(id);
        }

        public void Remove(Product product)
        {
            _db.Products.Remove(product);
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=> u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Seller = obj.Seller;
                obj.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}


