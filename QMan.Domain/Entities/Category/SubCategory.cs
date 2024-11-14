using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;

namespace QMan.Domain.Entities.Category;

public class SubCategory : BaseEntity
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public string? IconPath { get; set; }
    [JsonIgnore] public virtual Category Category { get; set; }
    [JsonIgnore] public virtual List<Product.Product> Products { get; set; }
    [JsonIgnore] public virtual List<Product.BaseProduct> BaseProducts { get; set; }
}