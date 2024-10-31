using AutoMapper;
using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Category;
using QMan.Application.Dtos.Product;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Business;
using QMan.Domain.Entities.Category;
using QMan.Domain.Entities.Product;

namespace QMan.Application.Mapper;

public class MapperContext:Profile
{
    public MapperContext()
    {
        CreateMap<AddAdminDto, Admin>();
        CreateMap<UpdateAddressDto, Address>();
        CreateMap<AddCategoryDto, Category>();
        CreateMap<AddSubCategoryDto, SubCategory>();
        CreateMap<AddProductDto, Product>();
        CreateMap<UpdateAddressDto, Address>();
    }
}