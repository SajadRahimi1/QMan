using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Login;
using QMan.Application.Interfaces;
using QMan.Application.Services.Cache;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Business;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class AdminRepository(
    AppDbContext appDbContext,
    IMapper mapper,
    IFileRepository fileRepository,
    ICacheService cacheService) : IAdminRepository
{
    private const string Section = "Admins";

    public async Task<BaseResponse> AddAdmin(AddAdminDto dto)
    {
        var admin = new Admin()
        {
            PhoneNumber = dto.PhoneNumber,
            IsActive = dto.IsActive,
            FullName = dto.FullName,
        };
        if (dto.Image is not null)
        {
            admin.ImageUrl = await fileRepository.SaveFileAsync(dto.Image, Section);
        }

        admin = appDbContext.Admins.Add(admin).Entity;
        await appDbContext.SaveChangesAsync();

        var access = GetAllAccess();
        foreach (var accessId in dto.Access)
        {
            if (access.Exists(a => a.Id == accessId))
            {
                await appDbContext.Database.ExecuteSqlAsync(
                    $"INSERT INTO dbo.AccessAdmin (AccessId, AdminsId) VALUES ({accessId}, {admin.Id});");
            }
        }

        // appDbContext.Admins.Update(admin);
        // await appDbContext.SaveChangesAsync();
        return new BaseResponse() { Data = admin };
    }

    public async Task<BaseResponse> GetAllAdmins(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var admins = await appDbContext.Admins.AsSplitQuery().AsNoTracking().Skip(skip)
            .Take(dto.PageSize).Select(a => new { a.ImageUrl, a.Id, a.FullName, a.PhoneNumber }).ToListAsync();

        return new BaseResponse() { Data = admins };
    }

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

        admin.PhoneNumber = dto.PhoneNumber;
        admin.FullName = dto.FullName;

        await appDbContext.Database.ExecuteSqlAsync(
            $"delete dbo.AccessAdmin where AdminsId = {admin.Id}");

        var access = GetAllAccess();
        foreach (var accessId in dto.Access)
        {
            if (access.Exists(a => a.Id == accessId))
            {
                await appDbContext.Database.ExecuteSqlAsync(
                    $"INSERT INTO dbo.AccessAdmin (AccessId, AdminsId) VALUES ({accessId}, {admin.Id});");
            }
        }

        if (dto.Image is not null)
        {
            admin.ImageUrl = await fileRepository.SaveFileAsync(dto.Image, Section);
        }

        appDbContext.Admins.Update(admin);
        appDbContext.SaveChanges();
        cacheService.Remove(CacheKeys.AccessCacheKey(admin.Id));
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

    public BaseResponse GetAllContactUs(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var comments = appDbContext.ContactUs.AsSplitQuery().AsNoTracking().Skip(skip).Take(dto.PageSize).ToList();

        return new BaseResponse() { Data = comments };
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
        var business = await appDbContext.Businesses.Include(b => b.Address)
            .FirstOrDefaultAsync(b => b.Id == dto.BusinessId);
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

    public async Task<BaseResponse> SendCode(SendCodeDto dto)
    {
        var admin = await appDbContext.Admins.FirstOrDefaultAsync(a => a.PhoneNumber == dto.PhoneNumber);
        if (admin is null) return new BaseResponse { StatusCode = 403, MessageSetter = "دسترسی به پنل ادمین ندارید" };

        if (!admin.IsActive)
            return new BaseResponse { StatusCode = 403, MessageSetter = "اکانت ادمین شما غیرفعال شده" };

        var code = new Random().Next(0, 9999);
        Console.Write(code.ToString());
        cacheService.Set(dto.PhoneNumber, code.ToString(), TimeSpan.FromMinutes(3));

        // todo: send code to sms provider
        return new BaseResponse();
    }

    public BaseResponse CheckCode(CheckCodeDto dto)
    {
        var savedCode = cacheService.Get<string>(dto.PhoneNumber);
        if (savedCode != dto.Code)
            return new BaseResponse()
                { StatusCode = 406, MessageSetter = "کد اشتباه است" };

        var admin = appDbContext.Admins.FirstOrDefault(a => a.PhoneNumber == dto.PhoneNumber);
        if (admin is null)
            return new BaseResponse()
                { StatusCode = 403 };
        return new BaseResponse() { Data = admin.Id };
    }

    public List<int> GetAdminAccesses(int adminId)
    {
        var cachedList = cacheService.Get<List<int>>(CacheKeys.AccessCacheKey(adminId));
        if (cachedList is not null) return cachedList;
        var accessList = appDbContext.Admins.Include(a => a.Access).FirstOrDefault(a => a.Id == adminId)
            ?.Access.Select(a => a.Id)
            .ToList();
        if (accessList is not null)
            cacheService.Set(CacheKeys.AccessCacheKey(adminId), accessList, TimeSpan.FromDays(1));
        return accessList ?? [];
    }

    public BaseResponse GetAllAccesses()
    {
        return new BaseResponse
        {
            Data = appDbContext.Accesses.Select(a => new
            {
                a.Title, a.Id
            }).ToList()
        };
    }

    private List<Access> GetAllAccess()
    {
        var cachedAccess = cacheService.Get<List<Access>>(CacheKeys.AllAccessCacheKey);
        if (cachedAccess is not null) return cachedAccess;
        var access = appDbContext.Accesses.ToList();
        cacheService.Set(CacheKeys.AllAccessCacheKey, access, TimeSpan.FromDays(15));
        return access;
    }
}