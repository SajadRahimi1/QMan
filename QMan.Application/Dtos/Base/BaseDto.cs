using System.Text.Json.Serialization;

namespace QMan.Application.Dtos.Base;

public class BaseDto
{
    [JsonIgnore] public int? UserId { get; set; }
    [JsonIgnore] public UserRole UserRole { get; set; } = UserRole.Business;
}