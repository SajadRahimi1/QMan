using QMan.Application.Dtos.Product;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Product;

namespace QMan.Application.Interfaces;

public interface IProductRepository
{
    Task<BaseResponse> GetAllProducts(int subCategoryId);
    Task<BaseResponse> AddProduct(AddProductDto dto);
    Task<BaseResponse> AddProducts(AddProductsDto dto);
    Task<BaseResponse> UpdateProduct(Product product);
}