using System.Text.Json.Serialization;

namespace QMan.Application.Dtos.Product;

public class AddProductsDto
{
    public List<int> ProductIds { get; set; }
    [JsonIgnore] public int? BusinessId { get; set; }    
}