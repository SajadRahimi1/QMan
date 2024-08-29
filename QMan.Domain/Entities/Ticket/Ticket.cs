using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Ticket;

public class Ticket : BaseEntity
{
    public string? Subject { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.New;
    public List<TicketMessage> Messages { get; set; } = new();
}

public enum TicketStatus
{
    Done,
    AdminWaiting,
    UserWaiting,
    OnReview,
    New,
}