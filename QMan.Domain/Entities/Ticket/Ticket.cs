using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Ticket;

public class Ticket : BaseEntity
{
    [MaxLength(150)] public string? Subject { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.New;
    public IEnumerable<TicketMessage> Messages { get; } = new List<TicketMessage>();
}

public enum TicketStatus
{
    Done,
    AdminWaiting,
    UserWaiting,
    OnReview,
    New,
}