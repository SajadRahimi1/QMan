using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using QMan.Domain.Entities.Admin;

namespace QMan.Application.Dtos.Admin;

public class AddAdminDto
{
    public IFormFile? Image { get; set; }
   [MaxLength(11)] public string PhoneNumber { get; set; }
   [MaxLength(60)] public string FullName { get; set; }
    public bool IsActive { get; set; } = true;
    public List<AccessEnum> Access { get; set; } = new();
}