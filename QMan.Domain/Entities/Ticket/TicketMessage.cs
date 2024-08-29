using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Ticket;

public class TicketMessage:BaseEntity
{
    public User.User? User { get; set; }
    public int UserId { get; set; }

    public Ticket? Ticket { get; set; }
    public int TicketId { get; set; }

    public string? Message { get; set; }
    public string? AttachmentLink { get; set; }
}