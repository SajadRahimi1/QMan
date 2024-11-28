using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.ContactUs;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;


public class HomeController(IHomeRepository homeRepository) : BaseController
{
    /// <summary>
    /// فرم ارتباط با ما
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult NewContactUs([FromQuery] NewContactUsDto dto) =>
        new BaseResult(homeRepository.NewContactUs(dto));
    
    /// <summary>
    /// دریافت تعرفه اشتراک
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllPlans() =>
        new BaseResult(homeRepository.GetAllPlans());
}