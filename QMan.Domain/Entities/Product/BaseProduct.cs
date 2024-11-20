using System.ComponentModel.DataAnnotations;
using QMan.Domain.Entities.Base;
using QMan.Domain.Entities.Category;

namespace QMan.Domain.Entities.Product;

public class BaseProduct : BaseEntity
{
    [MaxLength(50)] public string Title { get; set; }
    [MaxLength(20)] public string? Price { get; set; }
    [MaxLength(60)] public string? ImagePath { get; set; }
    public int SubcategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    public List<Business.Business> Businesses { get; set; }

}