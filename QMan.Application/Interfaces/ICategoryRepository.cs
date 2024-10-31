using QMan.Application.Dtos.Category;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface ICategoryRepository
{
    BaseResponse GetAllCategories();
    BaseResponse GetCategoryProducts();
    BaseResponse AddCategory(AddCategoryDto dto);
    BaseResponse AddSubCategory(AddSubCategoryDto dto);
}