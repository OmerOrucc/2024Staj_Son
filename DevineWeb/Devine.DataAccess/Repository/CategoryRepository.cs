using Devine.DataAccess.Repository.IRepository;
using DevineWeb.Data;
using DevineWeb.Models;
using Devine.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Devine.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

        // 'Get' metodunu implement edin
        public IEnumerable<Category> Get(Func<Category, bool> predicate, object parameters)
        {
            // params 'parameters' kullanımına göre işleyişi burada tanımlayın.
            // Örneğin, basit bir kullanım:
            return _db.Categories.Where(predicate).ToList();
        }

        Category? ICategoryRepository.Get(Func<Category, bool> value, object query)
        {
            throw new NotImplementedException();
        }
    }
}
