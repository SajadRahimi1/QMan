using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Product;
using QMan.Application.Interfaces;
using QMan.Application.Services.Cache;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Business;
using QMan.Domain.Entities.Product;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class BusinessRepository(AppDbContext appDbContext, IMapper mapper, ICacheService cacheService)
    : IBusinessRepository
{
    public async Task<BaseResponse> AddProduct(AddProductDto dto)
    {
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

    public Task<BaseResponse> SendCode(string phoneNumber)
    {
        var code = new Random().Next(0, 9999);
        cacheService.Set(phoneNumber, code.ToString(), TimeSpan.FromMinutes(3));

        // todo: send code to sms provider
        return Task.FromResult(new BaseResponse());
    }

    public BaseResponse CheckCode(string phoneNumber, string code)
    {
        var savedCode = cacheService.Get<string>(phoneNumber);
        if (savedCode != code)
            return new BaseResponse()
                { StatusCode = 406, MessageSetter = "لطفا دوباره برای ارسال کد تلاش کنید" };

        var business = appDbContext.Businesses.Add(new Business() { PhoneNumber = phoneNumber });
        appDbContext.SaveChanges();
        return new BaseResponse() { Data = business.Entity.Id };
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
}