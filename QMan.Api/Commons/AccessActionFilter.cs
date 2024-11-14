using Microsoft.AspNetCore.Mvc.Filters;
using QMan.Application.Dtos.Base;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Admin;
using QMan.Domain.Entities.Base;
using QMan.Infrastructure.Helpers;

namespace QMan.Api.Commons;

public class AccessActionFilter(AccessEnum access) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var adminRepository = context.HttpContext.RequestServices.GetService<IAdminRepository>();
        var token = context.HttpContext.Request.Headers.Authorization.ToString().Split(" ").Last();
        var userJwtModel = JwtHelper.GetUser(token);
        if (userJwtModel is { Role: UserRole.Admin, UserId: > 0 })
        {
            var exists = adminRepository?.GetAdminAccesses(userJwtModel.UserId)
                .Exists(accessId => accessId == (int)access);
            if (exists ?? false)
            {
                base.OnActionExecuting(context);
                return;
            }
        }

        context.Result = new BaseResult(new BaseResponse { StatusCode = 403 });
    }
}