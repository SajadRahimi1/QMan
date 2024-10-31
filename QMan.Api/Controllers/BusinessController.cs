using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Product;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;

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
}