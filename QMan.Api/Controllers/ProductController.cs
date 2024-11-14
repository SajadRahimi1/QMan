using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Category;
using QMan.Application.Dtos.Product;
using QMan.Application.Extensions;
using QMan.Application.Interfaces;

namespace QMan.Api.Controllers;

public class ProductController(IProductRepository productRepository):BaseController
{
    [HttpPost,Authorize(Roles = "Business")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto dto)
    {
        dto.BusinessId = UserJwtModel?.UserId;
        return new BaseResult(await productRepository.AddProduct(dto));
    }

    [HttpPost,Authorize(Roles = "Business")]
    public async Task<IActionResult> AddProducts([FromBody] AddProductsDto dto)
    {
        dto.BusinessId = UserJwtModel?.UserId;
        return new BaseResult(await productRepository.AddProducts(dto));
    }
}