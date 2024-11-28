using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface IMenuRepository
{
    Task<BaseResponse> GetSubCategories(int businessId);
    Task<BaseResponse> GetSelectedTheme(int businessId);
    Task<BaseResponse> GetAllProducts(int businessId);
    Task<BaseResponse> GetBusinessInformation(int businessId);
}