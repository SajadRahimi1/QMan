using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QMan.Application.Dtos.Product;

public class AddProductDto
{
    [Required, MaxLength(50)] public string Title { get; set; }
    [MaxLength(20)] public string? Price { get; set; }
    [Required] public int SubcategoryId { get; set; }
    [JsonIgnore] public int? BusinessId { get; set; }
}