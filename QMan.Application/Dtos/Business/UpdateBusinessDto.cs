using System.ComponentModel.DataAnnotations;
using QMan.Application.Dtos.Base;
using QMan.Domain.Entities.Business;

namespace QMan.Application.Dtos.Business;

public class UpdateBusinessDto
{
    [Required] public int BusinessId { get; set; }
    [MaxLength(100)] public string? Title { get; set; }
    [MaxLength(100)] public string? ManagerName { get; set; }
    [MaxLength(11)] public string? PhoneNumber { get; set; }
    [MaxLength(11)] public string? MobileNumber { get; set; }
    public UpdateAddressDto? Address { get; set; }
}

public class UpdateAddressDto
{
    [MaxLength(50)] public string Province { get; set; }
    [MaxLength(50)] public string City { get; set; }
    [MaxLength(300)] public string RestAddress { get; set; }
    [MaxLength(200)] public string? NeshanUrl { get; set; }
    [MaxLength(200)] public string? BaladUrl { get; set; }
    [MaxLength(200)] public string? GoogleMapUrl { get; set; }
    [MaxLength(100)] public string? InstagramUrl { get; set; }
    [MaxLength(100)] public string? WebsiteUrl { get; set; }
}