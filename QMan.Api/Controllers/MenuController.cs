using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;

public class MenuController(IMenuRepository menuRepository):BaseController
{
    /// <summary>
    /// دریافت تمامی دسته های دارای محصول
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllCategories([FromQuery] int id)
    {
        return new BaseResult(await menuRepository.GetSubCategories(id));
    }
    
    /// <summary>
    /// دریافت اطلاعات کافه
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetInformation([FromQuery] int id)
    {
        return new BaseResult(await menuRepository.GetBusinessInformation(id));
    }
    
    /// <summary>
    /// دریافت تم انتخاب شده
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetSelectedTheme([FromQuery] int id)
    {
        return new BaseResult(await menuRepository.GetSelectedTheme(id));
    }
    
    /// <summary>
    /// دریافت محصولات همراه با دسته
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] int id)
    {
        return new BaseResult(await menuRepository.GetAllProducts(id));
    }
}