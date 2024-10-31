using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.ContactUs;

public class ContactUs : BaseEntity
{
    [MaxLength(11)] public string? PhoneNumber { get; set; }
    [MaxLength(50)] public string? Name { get; set; }
    [MaxLength(100)] public string? CallTime { get; set; }
}