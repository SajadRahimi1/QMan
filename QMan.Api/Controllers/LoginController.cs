using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Login;
using QMan.Application.Interfaces;
using QMan.Infrastructure.Helpers;
using static System.Int32;

namespace QMan.Api.Controllers;

public class LoginController(IBusinessRepository businessRepository) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> SendCode([FromBody] SendCodeDto dto) =>
        new BaseResult(await businessRepository.SendCode(dto));

    [HttpPost]
    public IActionResult CheckCode([FromBody] CheckCodeDto dto)
    {
        var result = businessRepository.CheckCode(dto);
        if (result.StatusCode == 200)
        {
            if (TryParse(result.Data?.ToString(), out var userId))
                result.Token = JwtHelper.GenerateToken(new UserJwtModel() { UserId = userId ,Role = UserRole.User });
        }
        
        return new BaseResult(result);
    }
}