using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using QMan.Domain.Entities.Admin;

namespace QMan.Application.Dtos.Admin;

public class UpdateAdminDto
{
    [Required] public int AdminId { get; set; }
    public IFormFile? Image { get; set; }
    [MaxLength(11)][Required] public string PhoneNumber { get; set; }
    [MaxLength(60)][Required] public string FullName { get; set; }
    public List<AccessEnum> Access { get; set; } = new();
}