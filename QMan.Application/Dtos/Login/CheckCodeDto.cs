using System.ComponentModel.DataAnnotations;

namespace QMan.Application.Dtos.Login;

public class CheckCodeDto
{
    [Required, Length(11, 11)] public string PhoneNumber { get; set; }
    [Required] public string Code { get; set; }
}