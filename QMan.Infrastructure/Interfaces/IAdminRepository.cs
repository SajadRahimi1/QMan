using QMan.Application.Dtos.Admin;
using QMan.Domain.Entities.Base;

namespace QMan.Infrastructure.Interfaces;

public interface IAdminRepository
{
    Task<BaseResponse> AddAdmin(AddAdminDto dto);
    Task<BaseResponse> UpdateAdmin(UpdateAdminDto dto);
    Task<BaseResponse> ChangeAdminStatus(int adminId);
}