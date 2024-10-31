using System.ComponentModel.DataAnnotations;

namespace QMan.Application.Dtos.Product;

public class AddProductDto
{
    [Required,MaxLength(50)] public string Title { get; set; }
    [MaxLength(20)] public string? Price { get; set; }
    [Required] public int SubcategoryId { get; set; }
    public int? BusinessId { get; set; }
}