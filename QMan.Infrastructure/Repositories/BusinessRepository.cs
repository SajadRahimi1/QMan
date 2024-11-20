using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Login;
using QMan.Application.Dtos.Product;
using QMan.Application.Interfaces;
using QMan.Application.Services.Cache;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Business;
using QMan.Domain.Entities.Product;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class BusinessRepository(
    AppDbContext appDbContext,
    IMapper mapper,
    ICacheService cacheService,
    IFileRepository fileRepository)
    : IBusinessRepository
{
    public async Task<BaseResponse> AddProduct(AddProductDto dto)
    {
        if (dto.Image is not null)
        {
            dto.ImagePath = await fileRepository.SaveFileAsync(dto.Image, "Product");
        }

        var savedProduct = appDbContext.Products.Add(mapper.Map<Product>(dto));
        await appDbContext.SaveChangesAsync();
        return new BaseResponse() { Data = savedProduct.Entity };
    }

    public async Task<BaseResponse> AddProducts(AddProductsDto dto)
    {
        var business = await appDbContext.Businesses.FirstOrDefaultAsync(b => b.Id == dto.BusinessId);
        if (business is null) return new BaseResponse() { StatusCode = 404 };
        foreach (var productId in dto.ProductIds)
        {
            var product = await appDbContext.BaseProducts.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is not null) business.BaseProducts.Add(product);
        }

        appDbContext.Businesses.Update(business);
        await appDbContext.SaveChangesAsync();
        return new BaseResponse();
    }

    public Task<BaseResponse> SendCode(SendCodeDto dto)
    {
        var code = new Random().Next(0, 9999);
        Console.Write(code.ToString());
        cacheService.Set(dto.PhoneNumber, code.ToString(), TimeSpan.FromMinutes(3));

        // todo: send code to sms provider
        return Task.FromResult(new BaseResponse());
    }

    public BaseResponse CheckCode(CheckCodeDto dto)
    {
        var savedCode = cacheService.Get<string>(dto.PhoneNumber);
        if (savedCode != dto.Code)
            return new BaseResponse()
                { StatusCode = 406, MessageSetter = "کد اشتباه است" };

        var business = appDbContext.Businesses.FirstOrDefault(b => b.PhoneNumber == dto.PhoneNumber) ??
                       appDbContext.Businesses.Add(new Business() { PhoneNumber = dto.PhoneNumber }).Entity;

        appDbContext.SaveChanges();
        return new BaseResponse() { Data = business.Id };
    }

    public async Task<BaseResponse> UpdateInformation(UpdateBusinessDto dto)
    {
        var business = await appDbContext.Businesses.FirstOrDefaultAsync(b => b.Id == dto.BusinessId);
        if (business is null) return new BaseResponse() { StatusCode = 404 };
        business.Title = dto.Title ?? business.Title;
        business.ManagerName = dto.ManagerName ?? business.ManagerName;
        business.PhoneNumber = dto.PhoneNumber ?? business.PhoneNumber;
        business.ContactNumber = dto.ContactNumber ?? business.ContactNumber;
        appDbContext.Businesses.Update(business);
        await appDbContext.SaveChangesAsync();
        return new BaseResponse() { Data = business };
    }

    public async Task<BaseResponse> UpdateAddress(UpdateAddressDto dto)
    {
        var address = await appDbContext.Addresses.FirstOrDefaultAsync(a => a.BusinessId == dto.BusinessId);
        if (address is null)
        {
            appDbContext.Addresses.Add(mapper.Map<Address>(dto));
            await appDbContext.SaveChangesAsync();
        }

        address = mapper.Map<Address>(dto);
        appDbContext.Addresses.Update(address);
        await appDbContext.SaveChangesAsync();
        return new BaseResponse() { Data = address };
    }

    public async Task<BaseResponse> GetAllTicket(int businessId)
    {
        var ticket = await appDbContext.Tickets.AsSplitQuery().Include(b => b.Business).AsSplitQuery().AsNoTracking()
            .Select(t => new
            {
                t.Id,
                t.BusinessId,
                t.Business.Title,
                t.Status,
                t.Subject,
                t.UpdateDateTime,
            }).ToListAsync();

        return new BaseResponse() { Data = ticket };
    }

    public BaseResponse GetAllThemes()
    {
        var themes = appDbContext.Themes.Include(t => t.ThemeColors).Select(t => new
        {
            t.Id,
            t.EnglishTitle,
            t.PersianTitle,
            ThemeColors = t.ThemeColors.Select(tc => new
            {
                tc.Id,
                tc.EnglishTitle,
                tc.PersianTitle,
            })
        }).ToList();
        return new BaseResponse() { Data = themes };
    }

    public async Task<BaseResponse> SelectTheme(SelectThemeDto dto)
    {
        var business = await appDbContext.Businesses.FirstOrDefaultAsync(b => b.Id == dto.BusinessId);
        if (business is null) return new BaseResponse() { StatusCode = 404 };

        var themeColor = await appDbContext.ThemeColors.FirstOrDefaultAsync(tc => tc.Id == dto.ThemeColorId);
        if (themeColor is null) return new BaseResponse() { StatusCode = 404, MessageSetter = "تم انخاب شده یافت نشد" };

        business.SelectedThemeColorId = dto.ThemeColorId;
        appDbContext.Businesses.Update(business);
        await appDbContext.SaveChangesAsync();
        return new BaseResponse();
    }
}