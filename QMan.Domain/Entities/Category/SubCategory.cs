using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Category;

public class SubCategory:BaseEntity
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}