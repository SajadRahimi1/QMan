using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QMan.Application.Dtos.Business;

public class UpdateAddressDto
{
    [JsonIgnore] public int? BusinessId { get; set; }
    [MaxLength(50)] public string Province { get; set; }
    [MaxLength(50)] public string City { get; set; }
    [MaxLength(300)] public string RestAddress { get; set; }
    [MaxLength(200)] public string? NeshanUrl { get; set; }
    [MaxLength(200)] public string? BaladUrl { get; set; }
    [MaxLength(200)] public string? GoogleMapUrl { get; set; }
    [MaxLength(100)] public string? InstagramUrl { get; set; }
    [MaxLength(100)] public string? WebsiteUrl { get; set; }
}