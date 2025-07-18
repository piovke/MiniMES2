namespace Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}

public class CreateProductDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}