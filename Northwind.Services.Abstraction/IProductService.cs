using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Orders;
using Northwind.Contracts.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IProductService
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges);

        //craete 1 record with this code
        Task<ProductDto> GetProductById(int ProductId, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetProductPaged( int pageIndex, int pageSize, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetProductIdandPaged(int ProductId,int pageIndex, int pageSize, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetProductOnSales(bool trackChanges);

        Task<ProductOrderGroupDto> GetProductOrderOnSalesById(int productId, bool trackChanges);
        Task<ProductDto> GetProductPhotoById(int ProductId, bool trackChanges);
        ProductDto CreateProductId(ProductForCreatDto productForCreateDto);
        void CreateProductManyPhoto(ProductForCreatDto productForCreateDto,
            List<ProductPhotoForCreateDto> productPhotoForCreateDtos);
        void CreateOrder(OrderForCreateDto orderForCreateDto,
            OrderDetailForCreateDto orderDetailCreateDtos);
        void BuildOrder(OrderForCreateDto orderForCreateDto);
        void EditProductPhoto(ProductDto productDto, 
            List<ProductPhotoDto> productPhotoDto);
        void Insert(ProductForCreatDto productForCreatDto);
        void edit(ProductDto productDto);
        void remove(ProductDto productDto);
    }
}
