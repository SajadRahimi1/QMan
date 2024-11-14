using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Product;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;

[Authorize(Roles = "Business")]
public class BusinessController(IBusinessRepository businessRepository) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> UpdateInformation([FromBody] UpdateBusinessDto dto)
    {
        dto.BusinessId = UserJwtModel?.UserId;
        return new BaseResult(await businessRepository.UpdateInformation(dto));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressDto dto)
    {
        dto.BusinessId = UserJwtModel?.UserId;
        return new BaseResult(await businessRepository.UpdateAddress(dto));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto dto)
    {
        dto.BusinessId = UserJwtModel?.UserId;
        return new BaseResult(await businessRepository.AddProduct(dto));
    }

    [HttpPost]
    public async Task<IActionResult> AddProducts([FromBody] AddProductsDto dto)
    {
        dto.BusinessId = UserJwtModel?.UserId;
        return new BaseResult(await businessRepository.AddProducts(dto));
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets() =>
        new BaseResult(await businessRepository.GetAllTicket(UserJwtModel?.UserId ?? 0));
}