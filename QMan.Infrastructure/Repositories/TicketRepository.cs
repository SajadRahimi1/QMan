using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos;
using QMan.Application.Dtos.Ticket;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Ticket;
using QMan.Infrastructure.Contexts;
using QMan.Infrastructure.Interfaces;

namespace QMan.Infrastructure.Repositories;

public class TicketRepository(AppDbContext dbContext) : ITicketInterface
{
    public async Task<BaseResponse> CreateTicket(CreateTicketDto dto)
    {
        var ticket = dbContext.Tickets.AddAsync(new Ticket()
        {
            Subject = dto.Subject,
        }).Result.Entity;

        await dbContext.TicketMessages.AddAsync(new TicketMessage()
        {
            TicketId = ticket.Id,
            Message = dto.Message,
            UserId = dto.UserId,
            AttachmentLink = "",
        });

        return new BaseResponse();
    }

    public async Task<BaseResponse> GetAllTicket(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var ticket = await dbContext.Tickets.Skip(skip).Take(dto.PageSize).ToListAsync();

        return new BaseResponse() { Data = ticket };
    }

    public async Task<BaseResponse> GetTicketMessages(int ticketId)
    {
        var ticket = await dbContext.Tickets.Where(t => t.Id == ticketId).ToListAsync();
        return new BaseResponse() { Data = ticket };
    }

    public async Task<BaseResponse> NewTicketMessage(NewTicketMessageDto dto) =>
        new ()
        {
            Data = await dbContext.TicketMessages.AddAsync(new TicketMessage()
            {
                TicketId = dto.TicketId,
                Message = dto.Message,
                UserId = dto.UserId,
                AttachmentLink = "",
            })
        };
}