using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Business;

public class Business : BaseEntity
{
    [MaxLength(100)] public string? Title { get; set; }
    [MaxLength(100)] public string? ManagerName { get; set; }
    [MaxLength(11)] public string? PhoneNumber { get; set; }
    [MaxLength(11)] public string? ContactNumber { get; set; }
    public Address? Address { get; set; }
    public List<Comment.Comment> Comments { get; set; }=[];
    public List<Ticket.Ticket> Tickets { get; set; }=[];
    public List<Product.Product> Products { get; set; }=[];
    public List<Product.BaseProduct> BaseProducts { get; set; }=[];
}