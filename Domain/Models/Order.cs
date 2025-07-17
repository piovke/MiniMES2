namespace Domain.Models;
public class Order
{

    public int Id { get; set; }
    public string Code { get; set; } = "";
    public int MachineId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Machine Machine { get; set; } = null!;

    public Product Product { get; set; } = null!;
    
    public ICollection<Process> Processes { get; set; } = new List<Process>();
}