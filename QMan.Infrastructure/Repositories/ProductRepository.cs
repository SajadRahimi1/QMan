using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Product;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Product;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class ProductRepository(AppDbContext appDbContext) : IProductRepository
{
    public Task<BaseResponse> GetAllProducts(int subCategoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> AddProduct(AddProductDto dto)
    {
        var addedProduct = appDbContext.Products.Add(new Product()
        {
            SubcategoryId = dto.SubcategoryId,
            Price = dto.Price,
            Title = dto.Title,
            BusinessId = dto.BusinessId ?? 0,
            Description = dto.Description
        });

        await appDbContext.SaveChangesAsync();
        return new BaseResponse() { Data = addedProduct.Entity };
    }

    public async Task<BaseResponse> AddProducts(AddProductsDto dto)
    {
        var business = await appDbContext.Businesses.FirstOrDefaultAsync(b => b.Id == (dto.BusinessId ?? 0));
        foreach (var productId in dto.ProductIds)
        {
            var baseProduct =
                await appDbContext.BaseProducts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
            if (baseProduct is not null)
            {
                business?.BaseProducts.Add(baseProduct);
            }
        }

        appDbContext.Businesses.Update(business);
        await appDbContext.SaveChangesAsync();
        return new BaseResponse();
    }


    public Task<BaseResponse> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}