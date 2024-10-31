using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Business;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Business;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class AdminRepository
    (AppDbContext appDbContext, IMapper mapper, IFileRepository fileRepository) : IAdminRepository
{
    private const string Section = "Admins";

    public async Task<BaseResponse> AddAdmin(AddAdminDto dto)
    {
        var admin = mapper.Map<Admin>(dto);
        if (dto.Image is not null)
        {
            admin.ImageUrl = await fileRepository.SaveFileAsync(dto.Image, Section);
        }

        admin = appDbContext.Admins.Add(admin).Entity;
        return new BaseResponse() { Data = admin };
    }

    public async Task<BaseResponse> GetAllAdmins(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var admins = await appDbContext.Admins.AsSplitQuery().AsNoTracking().Skip(skip)
            .Take(dto.PageSize).Select(a => new { a.ImageUrl,a.Id,a.FullName,a.PhoneNumber }).ToListAsync();

        return new BaseResponse() { Data = admins };    }

    public async Task<BaseResponse> GetAdminInfo(int adminId)
    {
        var admin = await appDbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);

        return admin is null ? new BaseResponse() { StatusCode = 404 } : new BaseResponse() { Data = admin };
    }

    public async Task<BaseResponse> UpdateAdmin(UpdateAdminDto dto)
    {
        var admin = await appDbContext.Admins.FirstOrDefaultAsync(a => a.Id == dto.AdminId);
        if (admin is null)
        {
            return new BaseResponse() { StatusCode = 404 };
        }

        admin = mapper.Map<Admin>(dto);

        if (dto.Image is not null)
        {
            admin.ImageUrl = await fileRepository.SaveFileAsync(dto.Image, Section);
        }

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

    public async Task<BaseResponse> GetAllBusiness(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var businesses = await appDbContext.Businesses.Include(b => b.Address).AsSplitQuery().AsNoTracking().Skip(skip)
            .Take(dto.PageSize).Select(b => new { b.Address.City, b.Id, b.Title }).ToListAsync();

        return new BaseResponse() { Data = businesses };
    }

    public async Task<BaseResponse> GetBusinessInfo(int businessId)
    {
        var businesses = await appDbContext.Businesses.FirstOrDefaultAsync(b => b.Id == businessId);

        return businesses is null ? new BaseResponse() { StatusCode = 404 } : new BaseResponse() { Data = businesses };
    }

    public async Task<BaseResponse> UpdateBusiness(UpdateBusinessDto dto)
    {
        var business = await appDbContext.Businesses.Include(b=>b.Address).FirstOrDefaultAsync(b => b.Id == dto.BusinessId);
        if (business is null)
        {
            return new BaseResponse() { StatusCode = 404 };
        }

        // var address = mapper.Map<Address>(dto.Address);
        // address.BusinessId = business.Id;
        // address.Id = business.Address.Id;
        // appDbContext.Addresses.Update(address);
        //todo: update business properties
        return new BaseResponse() { StatusCode = 500 };
    }
}