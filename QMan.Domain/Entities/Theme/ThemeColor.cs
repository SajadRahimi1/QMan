using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Theme;

public class ThemeColor:BaseEntity
{
    [MaxLength(50)] public string? EnglishTitle { get; set; }
    [MaxLength(50)] public string? PersianTitle { get; set; }
    public Theme Theme { get; set; }
    public int ThemeId { get; set; }
}