using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Category;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Category;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext appDbContext, IFileRepository fileRepository)
    : ICategoryRepository
{
    private const string Section = "Categories";

    public async Task<BaseResponse> GetAllCategories()
    {
        return new BaseResponse() { Data = await appDbContext.Categories.Include(c => c.SubCategories).ToListAsync() };
    }

    public BaseResponse GetCategoryProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> AddCategory(AddCategoryDto dto)
    {
        var category = appDbContext.Categories.Add(new Category() { Title = dto.Title }).Entity;
        await appDbContext.SaveChangesAsync();

        foreach (var subCategory in dto.SubCategories ?? [])
        {
            string? iconPath = null;
            // subCategory.Icon is null
            // ? null
            // : await fileRepository.SaveFileAsync(subCategory.Icon, Section);
            appDbContext.SubCategories.Add(new SubCategory()
                { CategoryId = category.Id, Title = subCategory.Title, IconPath = iconPath });
        }

        await appDbContext.SaveChangesAsync();
        return new BaseResponse();
    }

    public async Task<BaseResponse> AddSubCategory(AddSubCategoryDto dto)
    {
        var subCategory = new SubCategory()
        {
            CategoryId = dto.CategoryId,
            Title = dto.Title,
        };
        if (dto.Icon is not null)
        {
            subCategory.IconPath = await fileRepository.SaveFileAsync(dto.Icon, Section);
        }

        subCategory = appDbContext.SubCategories.Add(subCategory).Entity;
        await appDbContext.SaveChangesAsync();
        return new BaseResponse() { Data = subCategory };
    }
}