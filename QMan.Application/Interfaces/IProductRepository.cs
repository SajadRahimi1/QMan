using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Product;

namespace QMan.Application.Interfaces;

public interface IProductRepository
{
    Task<BaseResponse> GetAllProducts(int subCategoryId);
    Task<BaseResponse> AddProduct(Product product);
    Task<BaseResponse> UpdateProduct(Product product);
}