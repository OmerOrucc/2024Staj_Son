using Devine.Models.Models;
using System.Collections.Generic;

namespace Devine.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Product? Find(int? id); // 'Product' döndürmeli
        void Remove(Product product); // 'Product' nesnesi almalı
        void Update(Product obj);
    }
}

