using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Orders;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Models;

namespace Northwind.Web.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CategoryForCreateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductForCreatDto>().ReverseMap();
            CreateMap<Order,OrderDto>().ReverseMap();

            CreateMap<ProductPhoto,ProductPhotoDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoForCreateDto>().ReverseMap();
        }
    }
}
