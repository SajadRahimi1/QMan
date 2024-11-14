using QMan.Application.Dtos.Category;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface ICategoryRepository
{
    Task<BaseResponse> GetAllCategories();
    BaseResponse GetCategoryProducts();
    Task<BaseResponse> AddCategory(AddCategoryDto dto);
    Task<BaseResponse> AddSubCategory(AddSubCategoryDto dto);
}