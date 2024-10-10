using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Category;

public class Category:BaseEntity
{
    public string Title { get; set; }
    public IEnumerable<SubCategory> SubCategories { get; set; }
}