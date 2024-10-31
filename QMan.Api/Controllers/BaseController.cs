using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QMan.Application.Dtos.Base;
using QMan.Infrastructure.Helpers;

namespace QMan.Api.Controllers;

[ApiController, Route("[controller]/[action]")]
public abstract class BaseController : Controller
{
    protected UserJwtModel? UserJwtModel;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        UserJwtModel = JwtHelper.GetUser(HttpContext.Request.Headers.Authorization);
        base.OnActionExecuting(context);
    }
}