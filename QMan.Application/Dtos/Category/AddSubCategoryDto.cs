using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace QMan.Application.Dtos.Category;

public class AddSubCategoryDto
{
    [Required] public string Title { get; set; }
    public IFormFile? Icon { get; set; }
    [Required] public int CategoryId { get; set; }
}