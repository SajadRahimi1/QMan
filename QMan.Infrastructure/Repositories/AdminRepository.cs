using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Admin;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Base;
using QMan.Infrastructure.Contexts;
using QMan.Infrastructure.Interfaces;

namespace QMan.Infrastructure.Repositories;

public class AdminRepository(AppDbContext appDbContext, IMapper mapper,IFileRepository fileRepository) : IAdminRepository
{
    private const string Section = "Admins";
    public async Task<BaseResponse> AddAdmin(AddAdminDto dto)
    {
        var admin = mapper.Map<Admin>(dto);
        if (dto.Image is not null)
        {
            admin.ImageUrl = await fileRepository.SaveFileAsync(dto.Image,Section);
        }
        admin = appDbContext.Admins.Add(admin).Entity;
        return new BaseResponse() { Data = admin };
    }

    public async Task<BaseResponse> UpdateAdmin(UpdateAdminDto dto)
    {
        var admin = await appDbContext.Admins.FirstOrDefaultAsync(a => a.Id == dto.AdminId);
        if (admin is null)
        {
            return new BaseResponse() { StatusCode = 404 };
        }

        admin = mapper.Map<Admin>(dto);
        appDbContext.Admins.Update(admin);
        appDbContext.SaveChanges();
        return new BaseResponse() { Data = admin };
    }

    public async Task<BaseResponse> ChangeAdminStatus(int adminId)
    {
        var admin = await appDbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);
        if (admin is null)
        {
            return new BaseResponse() { StatusCode = 404 };
        }

        admin.IsActive = !admin.IsActive;
        await appDbContext.Admins.ExecuteUpdateAsync(c => c.SetProperty(a => a.IsActive, admin.IsActive));
        return new BaseResponse() { Data = admin };
    }
}