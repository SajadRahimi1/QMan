using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.ContactUs;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface IHomeRepository
{
    BaseResponse NewContactUs(NewContactUsDto dto);
    BaseResponse GetAllPlans();
}