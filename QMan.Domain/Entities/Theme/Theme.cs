using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Theme;

public class Theme : BaseEntity
{
    [MaxLength(50)] public string? EnglishTitle { get; set; }
    [MaxLength(50)] public string? PersianTitle { get; set; }
    [JsonIgnore] public virtual ICollection<ThemeColor> ThemeColors { get; set; } = [];

}