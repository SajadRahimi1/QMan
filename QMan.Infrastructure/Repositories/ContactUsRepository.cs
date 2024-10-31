using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.ContactUs;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.ContactUs;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class ContactUsRepository(AppDbContext appDbContext):IContactUsRepository
{
    public BaseResponse NewContactUs(NewContactUsDto dto)
    {
        appDbContext.ContactUs.Add(new ContactUs()
        {
            Name = dto.Name,
            PhoneNumber = dto.PhoneNumber,
            CallTime = dto.CallTime,
        });
        appDbContext.SaveChanges();
        return new BaseResponse();
    }

    public BaseResponse GetAllContactUs(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var comments = appDbContext.ContactUs.AsSplitQuery().AsNoTracking().Skip(skip).Take(dto.PageSize).ToList();

        return new BaseResponse() { Data = comments };
    }
}