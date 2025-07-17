namespace Domain.Models;
public class Parameter
{
    public int Id;
    public string Name {get; set;} = "";
    public string Unit { get; set; }="";
    
    public ICollection<ProcessParameters> ProcessParameters { get; set; } = new List<ProcessParameters>();
}