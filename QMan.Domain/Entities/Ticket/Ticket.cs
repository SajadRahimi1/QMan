using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Ticket;

public class Ticket : BaseEntity
{
    [MaxLength(150)] public string? Subject { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Support;
    public IEnumerable<TicketMessage> Messages { get; } = new List<TicketMessage>();
}

public enum TicketStatus
{
    Dev,
    Financial,
    Support,
    Management,
    Product,
    Deleted,
    Waiting,
    Done,
}