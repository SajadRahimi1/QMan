using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.ContactUs;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;


public class HomeController(IContactUsRepository contactUsRepository) : BaseController
{
    [HttpPost]
    public IActionResult NewContactUs([FromQuery] NewContactUsDto dto) =>
        new BaseResult(contactUsRepository.NewContactUs(dto));
}