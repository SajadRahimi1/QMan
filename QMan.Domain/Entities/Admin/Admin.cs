using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Admin;

public class Admin : BaseEntity
{
    public string? ImageUrl { get; set; }
    [MaxLength(11)] public string PhoneNumber { get; set; }
    [MaxLength(60)] public string FullName { get; set; }
    public bool IsActive { get; set; } = true;

    [JsonIgnore] public List<Access> Access { get; set; } = new();
}