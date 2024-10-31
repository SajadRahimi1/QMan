using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos.Category;
using QMan.Application.Dtos.ContactUs;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;

[ApiController, Route("[controller]/[action]")]
public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
{
    [HttpPost, Consumes("multipart/form-data")]
    public IActionResult NewCategory([FromForm] AddCategoryDto dto) =>
        new BaseResult(categoryRepository.AddCategory(dto));
    
    
}