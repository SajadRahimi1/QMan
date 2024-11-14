using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Ticket;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Business;
using QMan.Domain.Entities.Ticket;
using QMan.Infrastructure.Contexts;

namespace QMan.Infrastructure.Repositories;

public class TicketRepository(AppDbContext dbContext, IFileRepository fileRepository) : ITicketRepository
{
    private const string Section = "Tickets";

    public async Task<BaseResponse> CreateTicket(CreateTicketDto dto)
    {
        var ticket = new Ticket()
        {
            Subject = dto.Subject,
            BusinessId = dto.UserId ?? 0,
        };

        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync();


        await dbContext.TicketMessages.AddAsync(new TicketMessage()
        {
            TicketId = ticket.Id,
            Message = dto.Message,
            BusinessId = dto.UserId,
            AttachmentLink = dto.Attachment is null ? null : await fileRepository.SaveFileAsync(dto.Attachment, Section)
        });

        await dbContext.SaveChangesAsync();

        return new BaseResponse() { Data = ticket };
    }

    public async Task<BaseResponse> GetAllTicket(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var ticket = await dbContext.Tickets.AsSplitQuery().Include(b => b.Business).AsSplitQuery().AsNoTracking()
            .Skip(skip)
            .Take(dto.PageSize).Select(t => new
            {
                t.Id,
                t.BusinessId,
                BusinessTitle = t.Business.Title,
                t.Status,
                t.Subject,
                UpdateDateTime = t.UpdateDateTime ?? t.CreatedDateTime,
            }).ToListAsync();

        return new BaseResponse() { Data = ticket };
    }

    public async Task<BaseResponse> GetTicketMessages(int ticketId)
        => new()
        {
            Data = await dbContext.Tickets.AsNoTracking().AsSplitQuery().Where(t => t.Id == ticketId)
                .Include(t => t.Messages)
                .Include(t => t.Business)
                .ToListAsync()
        };


    public async Task<BaseResponse> NewTicketMessage(NewTicketMessageDto dto)
    {
        var ticketMessage = await dbContext.TicketMessages.AddAsync(new TicketMessage()
        {
            TicketId = dto.TicketId,
            Message = dto.Message,
            BusinessId = dto.UserRole == UserRole.Business ? dto.UserId : null,
            AdminId = dto.UserRole == UserRole.Admin ? dto.UserId : null,
            AttachmentLink = dto.Attachment is null ? null : await fileRepository.SaveFileAsync(dto.Attachment, Section)
        });
        await dbContext.SaveChangesAsync();

        await dbContext.Tickets.AsNoTracking().Where(t => t.Id == dto.TicketId)
            .ExecuteUpdateAsync(p => p.SetProperty(t => t.Status, dto.Status));
        return new BaseResponse() { Data = ticketMessage.Entity };
    }

    public async Task<BaseResponse> ChangeTicketStatus(ChangeTicketStatusDto dto)
    {
        var ticket = await dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == dto.TicketId);
        if (ticket is null) return new BaseResponse() { StatusCode = 404 };
        await dbContext.Tickets.AsNoTracking().Where(t => t.Id == dto.TicketId)
            .ExecuteUpdateAsync(p => p.SetProperty(t => t.Status, dto.Status));

        return new BaseResponse();
    }
}