using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Plans;

public class Plan:BaseEntity
{
    public string? Title { get; set; }
    public int? ExpirationDay { get; set; }
    public string? Price { get; set; }
}