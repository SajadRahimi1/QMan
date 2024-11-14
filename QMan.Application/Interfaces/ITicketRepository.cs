using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Ticket;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface ITicketRepository
{
    public Task<BaseResponse> CreateTicket(CreateTicketDto dto);
    public Task<BaseResponse> GetAllTicket(PaginationBaseDto dto);
    
    public Task<BaseResponse> GetTicketMessages(int ticketId);
    public Task<BaseResponse> NewTicketMessage(NewTicketMessageDto dto);

    public Task<BaseResponse> ChangeTicketStatus(ChangeTicketStatusDto dto);
}