using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Login;
using QMan.Application.Dtos.Product;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface IBusinessRepository
{
    Task<BaseResponse> AddProduct(AddProductDto dto);
    Task<BaseResponse> AddProducts(AddProductsDto dto);

    Task<BaseResponse> SendCode(SendCodeDto dto);
    BaseResponse CheckCode(CheckCodeDto dto);
    Task<BaseResponse> UpdateInformation(UpdateBusinessDto dto);
    Task<BaseResponse> UpdateAddress(UpdateAddressDto dto);
    Task<BaseResponse> GetAllTicket(int businessId);
    BaseResponse GetAllThemes();
    Task<BaseResponse> SelectTheme(SelectThemeDto dto);
}