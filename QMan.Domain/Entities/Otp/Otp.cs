using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Otp;

public class Otp : BaseEntity
{
    [MaxLength(11)] public string PhoneNumber { get; set; }
    [MaxLength(6)] public string Code { get; set; }
}