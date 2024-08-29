using Microsoft.AspNetCore.Http;
using QMan.Application.Dtos.Base;

namespace QMan.Application.Dtos.Ticket;

public class NewTicketMessageDto:BaseDto
{
    public int TicketId { get; set; }
    public string? Message { get; set; }
    public IFormFile? Attachment { get; set; }
}