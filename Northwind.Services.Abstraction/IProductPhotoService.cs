using Northwind.Contracts.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IProductPhotoService
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<ProductPhotoDto>> GetAllProductPhoto(bool trackChanges);

        //craete 1 record with this code
        Task<ProductPhotoDto> GetProductPhotoById(int ProductPhotoId, bool trackChanges);
        //Task<IEnumerable<ProductPhoto>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges);
        void insert(ProductForCreatDto productForCreatDto);
        void edit(ProductPhotoDto productPhotoDto);
        void remove(ProductPhotoDto productPhotoDto);
    }
}
