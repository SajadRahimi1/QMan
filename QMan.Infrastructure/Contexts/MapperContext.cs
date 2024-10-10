using AutoMapper;
using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Business;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Business;

namespace QMan.Infrastructure.Contexts;

public class MapperContext:Profile
{
    public MapperContext()
    {
        CreateMap<AddAdminDto, Admin>();
        CreateMap<UpdateAddressDto, Address>();
    }
}