using Microsoft.AspNetCore.Http;
using QMan.Application.Dtos.Base;

namespace QMan.Application.Dtos.Ticket;

public class CreateTicketDto : BaseDto
{
    public string? Subject { get; set; }
    public string? Message { get; set; }
    public IFormFile? Attachment { get; set; }
}