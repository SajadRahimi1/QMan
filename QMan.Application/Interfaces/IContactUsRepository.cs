using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.ContactUs;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface IContactUsRepository
{
    BaseResponse NewContactUs(NewContactUsDto dto);
    BaseResponse GetAllContactUs(PaginationBaseDto dto);
}