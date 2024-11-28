namespace QMan.Application.Dtos.Product;

public class GetProductsDto
{
    public List<SubCategories> SubCategories { get; set; } = [];
}

public class SubCategories
{
    public string? Title { get; set; }
    public int SubCategoryId { get; set; }
    public string? IconPath { get; set; }
    public List<Products> Products { get; set; } = [];
}

public class Products
{
    public string? Title { get; set; }
    public string? Price { get; set; }
    public string? ImagePath { get; set; }
}