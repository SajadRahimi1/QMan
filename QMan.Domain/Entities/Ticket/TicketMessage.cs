using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Ticket;

public class TicketMessage : BaseEntity
{
    [JsonIgnore] public User.User? User { get; set; }
    public int? UserId { get; set; }
    public int TicketId { get; set; }
    [JsonIgnore] public Ticket Ticket { get; set; } = null!;
    public string? Message { get; set; }
    public string? AttachmentLink { get; set; }
}