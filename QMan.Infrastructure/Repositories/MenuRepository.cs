using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Product;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Category;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class MenuRepository(AppDbContext appDbContext) : IMenuRepository
{
    public async Task<BaseResponse> GetSubCategories(int businessId)
    {
        var business = await appDbContext.Businesses.Include(b => b.BaseProducts).ThenInclude(bp => bp.SubCategory)
            .Include(b => b.Products).ThenInclude(bp => bp.SubCategory).FirstOrDefaultAsync(b => b.Id == businessId);
        if (business == null)
        {
            return new BaseResponse() { StatusCode = 404 };
        }

        List<SubCategory> subCategories = [];
        subCategories.AddRange(business.BaseProducts.Select(bp => bp.SubCategory));
        subCategories.AddRange(business.Products.Select(p => p.SubCategory));

        var cleanedSubCategories = subCategories.Distinct();
        return new BaseResponse() { Data = cleanedSubCategories, StatusCode = 200 };
    }

    public async Task<BaseResponse> GetSelectedTheme(int businessId)
    {
        var business = await appDbContext.Businesses.Include(b => b.ThemeColor)
            .FirstOrDefaultAsync(b => b.Id == businessId);
        return new BaseResponse() { Data = business?.ThemeColor?.EnglishTitle };
    }

    public async Task<BaseResponse> GetAllProducts(int businessId)
    {
        var business = await appDbContext.Businesses.Include(b => b.BaseProducts).ThenInclude(bp => bp.SubCategory)
            .Include(b => b.Products).ThenInclude(bp => bp.SubCategory).FirstOrDefaultAsync(b => b.Id == businessId);
        if (business == null)
        {
            return new BaseResponse() { StatusCode = 404 };
        }

        var returnData = new GetProductsDto();
        foreach (var product in business.Products)
        {
            var subcategory = returnData.SubCategories.FirstOrDefault(sc => sc.SubCategoryId == product.SubcategoryId);
            if (subcategory is null)
            {
                subcategory = new SubCategories()
                {
                    SubCategoryId = product.SubCategory.Id,
                    Title = product.SubCategory.Title,
                    IconPath = product.SubCategory.IconPath
                };
                returnData.SubCategories.Add(subcategory);
            }

            subcategory.Products.AddRange(business.Products.Where(p => p.SubcategoryId == subcategory.SubCategoryId)
                .Select(p => new Products()
                {
                    Title = p.Title, Price = p.Price, ImagePath = p.ImagePath
                }));
            subcategory.Products.AddRange(business.BaseProducts.Where(p => p.SubcategoryId == subcategory.SubCategoryId)
                .Select(p => new Products()
                {
                    Title = p.Title, Price = p.Price, ImagePath = p.ImagePath
                }));
        }

        return new BaseResponse() { Data = returnData, StatusCode = 200 };
    }

    public async Task<BaseResponse> GetBusinessInformation(int businessId)
    {
        var business = await appDbContext.Businesses.Include(b => b.Address).Select(b => new
            {
                b.Id,
                b.Title,
                b.Address.City,
                b.Address.RestAddress,
                b.Address.NeshanUrl,
                b.Address.BaladUrl,
                b.Address.GoogleMapUrl,
                b.Address.InstagramUrl,
                b.Address.WebsiteUrl,
            })
            .FirstOrDefaultAsync(b => b.Id == businessId);
        return new BaseResponse() { Data = business };
    }
}