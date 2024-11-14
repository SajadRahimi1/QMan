using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Ticket;

public class TicketMessage : BaseEntity
{
    [JsonIgnore] public Business.Business? Business { get; set; }
    public int? BusinessId { get; set; }
    public int? AdminId { get; set; }
    [JsonIgnore]  public int TicketId { get; set; }
    [JsonIgnore] public Ticket Ticket { get; set; } = null!;
    public string? Message { get; set; }
    [MaxLength(100)] public string? AttachmentLink { get; set; }
}