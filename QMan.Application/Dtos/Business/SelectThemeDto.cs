using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QMan.Application.Dtos.Business;

public record SelectThemeDto
{
  [JsonIgnore]  public int BusinessId { get; set; }
  [Required]  public int ThemeColorId { get; set; }
}