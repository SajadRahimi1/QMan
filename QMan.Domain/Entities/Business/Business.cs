using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Theme;

namespace QMan.Domain.Entities.Business;

public class Business : BaseEntity
{
    [MaxLength(100)] public string? Title { get; set; }
    [MaxLength(100)] public string? ManagerName { get; set; }
    [MaxLength(11)] public string? PhoneNumber { get; set; }
    [MaxLength(11)] public string? ContactNumber { get; set; }
    public int? SelectedThemeColorId { get; set; }
    public ThemeColor? ThemeColor { get; set; }
    public Address? Address { get; set; }
    public List<Comment.Comment> Comments { get; set; } = [];
    public List<Ticket.Ticket> Tickets { get; set; } = [];
    public List<Product.Product> Products { get; set; } = [];
    public List<Product.BaseProduct> BaseProducts { get; set; } = [];
}