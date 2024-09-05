using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using QMan.Application.Dtos.Base;
using QMan.Domain.Entities.Ticket;

namespace QMan.Application.Dtos.Ticket;

public class NewTicketMessageDto:BaseDto
{
    public int TicketId { get; set; }
    public string? Message { get; set; }
    public IFormFile? Attachment { get; set; }
    [JsonIgnore] public TicketStatus Status { get; set; } = TicketStatus.UserWaiting;
}