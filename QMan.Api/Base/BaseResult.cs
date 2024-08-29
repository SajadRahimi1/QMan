using Microsoft.AspNetCore.Mvc;
using QMan.Domain.Entities.Base;

namespace QMan.Api.Base;

public class BaseResult(BaseResponse response) : IActionResult
{
    public Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(response)
        {
            StatusCode = response.StatusCode,
        };

        return objectResult.ExecuteResultAsync(context);
    }
}