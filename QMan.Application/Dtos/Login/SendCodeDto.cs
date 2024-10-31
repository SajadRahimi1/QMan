using System.ComponentModel.DataAnnotations;

namespace QMan.Application.Dtos.Login;

public class SendCodeDto
{
    [Required,Length(11,11)] public string PhoneNumber { get; set; }
}