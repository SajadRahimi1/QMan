using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.User;

public class User:BaseEntity
{
   [MaxLength(150)] public string? Name { get; set; }
   [MaxLength(11)] public string? PhoneNumber { get; set; }
   [MaxLength(300)] public string? Address { get; set; }
   [MaxLength(50)] public string? Telegram { get; set; }
   [MaxLength(50)] public string? Instagram { get; set; }
   [MaxLength(100)] public string? Website { get; set; }
}