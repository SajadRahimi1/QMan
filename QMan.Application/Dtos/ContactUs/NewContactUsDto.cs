using System.ComponentModel.DataAnnotations;

namespace QMan.Application.Dtos.ContactUs;

public class NewContactUsDto
{
    [MaxLength(11)] public string? PhoneNumber { get; set; }
    [MaxLength(50)] public string? Name { get; set; }
    [MaxLength(100)] public string? CallTime { get; set; }
}