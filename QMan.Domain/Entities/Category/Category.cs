using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Category;

public class Category:BaseEntity
{
    public string Title { get; set; }
    public virtual ICollection<SubCategory> SubCategories { get; set; } = [];
}