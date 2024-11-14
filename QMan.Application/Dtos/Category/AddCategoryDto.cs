using Microsoft.AspNetCore.Http;

namespace QMan.Application.Dtos.Category;

public class AddCategoryDto
{
    public string Title { get; set; }
    public List<SubCategoryDto>? SubCategories { get; set; }
}

public class SubCategoryDto
{
    public string Title { get; set; }
    // public IFormFile? Icon { get; set; }
}