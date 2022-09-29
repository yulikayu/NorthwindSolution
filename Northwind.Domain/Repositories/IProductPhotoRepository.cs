using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IProductPhotoRepository
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<ProductPhoto>> GetAllProductPhoto(bool trackChanges);

        //craete 1 record with this code
        Task<ProductPhoto> GetProductPhotoById(int ProductPhotoId, bool trackChanges);
        //Task<IEnumerable<ProductPhoto>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges);
        void insert(ProductPhoto productPhoto);
        void edit(ProductPhoto productPhoto);
        void remove(ProductPhoto productPhoto);
    }
}
