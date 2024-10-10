using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Business;

public class Address : BaseEntity
{
    [MaxLength(50)] public string Province { get; set; }
    [MaxLength(50)] public string City { get; set; }
    [MaxLength(300)] public string RestAddress { get; set; }
    [MaxLength(200)] public string? NeshanUrl { get; set; }
    [MaxLength(200)] public string? BaladUrl { get; set; }
    [MaxLength(200)] public string? GoogleMapUrl { get; set; }
    [MaxLength(100)] public string? InstagramUrl { get; set; }
    [MaxLength(100)] public string? WebsiteUrl { get; set; }
    public Business Business { get; set; }
    public int BusinessId { get; set; }
}