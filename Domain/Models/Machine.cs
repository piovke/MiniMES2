namespace Domain.Models;

public class Machine
{
    public int Id {get; set;}
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}