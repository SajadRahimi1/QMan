using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Admin;

public class Access : BaseEntity
{
    public string? Title { get; set; }
    [JsonIgnore] public List<Admin> Admins { get; set; } = new();
}