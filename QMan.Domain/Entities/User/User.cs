using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.User;

public class User:BaseEntity
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Telegram { get; set; }
    public string? Instagram { get; set; }
    public string? Website { get; set; }
}