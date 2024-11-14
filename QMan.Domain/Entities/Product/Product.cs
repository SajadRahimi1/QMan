using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Category;

namespace QMan.Domain.Entities.Product;

public class Product : BaseEntity
{
    [MaxLength(50)] public string Title { get; set; }
    [MaxLength(20)] public string? Price { get; set; }
    public int SubcategoryId { get; set; }
    [JsonIgnore] public virtual SubCategory SubCategory { get; set; }

    public int BusinessId { get; set; }
    [JsonIgnore] public virtual Business.Business Business { get; set; }
}