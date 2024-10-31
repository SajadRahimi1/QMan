using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Category;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Category;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext appDbContext, IFileRepository fileRepository,IMapper mapper) : ICategoryRepository
{
    public BaseResponse GetAllCategories()
    {
        return new BaseResponse() { Data = appDbContext.Categories.Include(c => c.SubCategories).ToList() };
    }

    public BaseResponse GetCategoryProducts()
    {
        throw new NotImplementedException();
    }

    public BaseResponse AddCategory(AddCategoryDto dto)
    {
        var category = appDbContext.Categories.Add(mapper.Map<Category>(dto));
        return new BaseResponse() { Data = category.Entity };
    }

    public BaseResponse AddSubCategory(AddSubCategoryDto dto)
    {
        var category = appDbContext.SubCategories.Add(mapper.Map<SubCategory>(dto));
        return new BaseResponse() { Data = category.Entity };
    }
}