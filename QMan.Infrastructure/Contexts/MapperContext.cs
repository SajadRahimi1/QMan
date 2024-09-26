using AutoMapper;
using QMan.Application.Dtos.Admin;
using QMan.Domain.Entities.Admin;

namespace QMan.Infrastructure.Contexts;

public class MapperContext:Profile
{
    public MapperContext()
    {
        CreateMap<AddAdminDto, Admin>();
    }
}