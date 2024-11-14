using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.Category;
using QMan.Application.Dtos.ContactUs;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;


public class CategoryController(ICategoryRepository categoryRepository) : BaseController
{
    [HttpPost]
    // [Consumes("multipart/form-data")]
    public async Task<IActionResult> NewCategory([FromBody] AddCategoryDto dto) =>
        new BaseResult(await categoryRepository.AddCategory(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllCategories() =>
        new BaseResult(await categoryRepository.GetAllCategories());
}