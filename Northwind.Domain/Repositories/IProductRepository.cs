using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IProductRepository
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<Product>> GetAllProduct(bool trackChanges);

        //craete 1 record with this code
        Task<Product> GetProductById(int ProductId, bool trackChanges);
        Task<IEnumerable<Product>> GetProductPaged(int pageIndex,int pageSize, bool trackChanges);
        Task<IEnumerable<Product>> GetProductIdandPaged(int ProductId, int pageIndex, int pageSize, bool trackChanges);
        Task<Product> GetProductPhotoById(int ProductId, bool trackChanges);
        Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges);
        void insert(Product product);
        void edit(Product product);
        void remove(Product product);
    }
}
