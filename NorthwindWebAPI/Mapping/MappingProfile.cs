using AutoMapper;
using Northwind.Contracts.Dto.Authentication;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Orders;
using Northwind.Contracts.Dto.Product;
using Northwind.Contracts.Dto.Supplier;
using Northwind.Domain.Models;

namespace Northwind.WebAPI.Mapping
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
            CreateMap<Order, OrderForCreateDto>().ReverseMap();

            CreateMap<ProductPhoto,ProductPhotoDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoForCreateDto>().ReverseMap();

            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierForCreateDto>().ReverseMap();

            CreateMap<OrderDetail,OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail,OrderDetailForCreateDto>().ReverseMap();
            CreateMap<Product, ProductOrderGroupDto>().ReverseMap();

            CreateMap<UserRegistrationDto,User>()
                .ForMember(u=>u.UserName,opt=>opt.MapFrom(x=>x.Email));
            CreateMap<UserLoginDto, User>().ReverseMap();
        }
    }
}
