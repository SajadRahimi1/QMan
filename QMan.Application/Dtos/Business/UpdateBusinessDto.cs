using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QMan.Application.Dtos.Business;

public class UpdateBusinessDto
{
    [JsonIgnore] public int? BusinessId { get; set; }
    [MaxLength(100)] public string? Title { get; set; }
    [MaxLength(100)] public string? ManagerName { get; set; }
    [MaxLength(11)] public string? PhoneNumber { get; set; }
    [MaxLength(11)] public string? ContactNumber { get; set; }
}

