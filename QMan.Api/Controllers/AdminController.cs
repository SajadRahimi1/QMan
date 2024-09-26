using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos.Admin;
using QMan.Infrastructure.Interfaces;

namespace QMan.Api.Controllers;

[ApiController(), Route("[controller]/[action]")]
public class AdminController(IAdminRepository adminRepository) : ControllerBase
{
    [HttpPost,Consumes("multipart/form-data")]
    public async Task<IActionResult> AddAdmin([FromForm] AddAdminDto dto) =>
        new BaseResult(await adminRepository.AddAdmin(dto));
}