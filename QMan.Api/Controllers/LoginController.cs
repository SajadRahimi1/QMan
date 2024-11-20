using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Login;
using QMan.Application.Interfaces;
using QMan.Infrastructure.Helpers;
using static System.Int32;

namespace QMan.Api.Controllers;


public class LoginController(IBusinessRepository businessRepository) : BaseController
{
    /// <summary>
    /// خواستی کد بفرستی برای کسب و کار ها اینو استفاده کن
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    ///     /// <response code="200">کد ارسال شده برو بعدی. (البته اگه هم ارسال نشه هم 200 میگه)</response>
    [HttpPost]
    public async Task<IActionResult> SendCode([FromBody] SendCodeDto dto) =>
        new BaseResult(await businessRepository.SendCode(dto));

    /// <summary>
    /// بررسی کد ارسال شده
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <response code="406">این یعنی کد اشتباهه</response>
    /// <response code="200">همچی اوکیه. یادت نره توکن رو سیو کنی</response>
    [HttpPost]
    public IActionResult CheckCode([FromBody] CheckCodeDto dto)
    {
        var result = businessRepository.CheckCode(dto);
        if (result.StatusCode == 200)
        {
            if (TryParse(result.Data?.ToString(), out var userId))
                result.Token = JwtHelper.GenerateToken(new UserJwtModel() { UserId = userId ,Role = UserRole.Business });
        }
        
        return new BaseResult(result);
    }
}