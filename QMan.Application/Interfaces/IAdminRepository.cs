using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Login;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface IAdminRepository
{
    Task<BaseResponse> AddAdmin(AddAdminDto dto);
    Task<BaseResponse> GetAllAdmins(PaginationBaseDto dto);
    Task<BaseResponse> GetAdminInfo(int adminId);
    Task<BaseResponse> UpdateAdmin(UpdateAdminDto dto);
    Task<BaseResponse> ChangeAdminStatus(int adminId);
    Task<BaseResponse> GetAllBusiness(PaginationBaseDto dto);
    Task<BaseResponse> GetBusinessInfo(int businessId);
    Task<BaseResponse> UpdateBusiness(UpdateBusinessDto dto);
    Task<BaseResponse> SendCode(SendCodeDto dto);
    BaseResponse CheckCode(CheckCodeDto dto);
    List<int> GetAdminAccesses(int adminId);
    BaseResponse GetAllAccesses();
    
    BaseResponse GetAllContactUs(PaginationBaseDto dto);

}