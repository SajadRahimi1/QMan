using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace QMan.Application.Dtos.Product;

public class AddProductDto
{
    [Required, MaxLength(50)] public string Title { get; set; }
    [MaxLength(20)] public string? Price { get; set; }
    [Required] public int SubcategoryId { get; set; }
    [JsonIgnore] public int? BusinessId { get; set; }
    [JsonIgnore] public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; }
}