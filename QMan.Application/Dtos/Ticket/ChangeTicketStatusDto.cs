using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Ticket;

namespace QMan.Application.Dtos.Ticket;

public class ChangeTicketStatusDto
{
    [Required] public TicketStatus Status { get; set; }
    [Required] public int TicketId { get; set; }
}