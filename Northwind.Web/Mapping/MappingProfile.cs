using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Models;

namespace Northwind.Web.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CategoryForCreateDto>().ReverseMap();
        }
    }
}
