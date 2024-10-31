using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Internal;

namespace QMan.Application.Dtos.Category;

public class AddSubCategoryDto
{
   [Required] public string Title { get; set; }
    public FormFile? Icon { get; set; }
   [Required] public int CategoryId { get; set; }
}