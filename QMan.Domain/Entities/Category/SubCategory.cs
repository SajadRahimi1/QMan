using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Category;

public class SubCategory:BaseEntity
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public string IconPath { get; set; }
    public Category Category { get; set; }
    public List<Product.Product> Products { get; set; }
    public List<Product.BaseProduct> BaseProducts { get; set; }
}