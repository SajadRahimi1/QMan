using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Product;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface IBusinessRepository
{
    Task<BaseResponse> AddProduct(AddProductDto dto);
    Task<BaseResponse> AddProducts(AddProductsDto dto);
    
    Task<BaseResponse> SendCode(string phoneNumber);    
    BaseResponse CheckCode(string phoneNumber, string code);
    Task<BaseResponse> UpdateInformation(UpdateBusinessDto dto);
    Task<BaseResponse> UpdateAddress(UpdateAddressDto dto);

}