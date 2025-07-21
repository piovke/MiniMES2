namespace Application.DTOs;

public class ProcessParameterDto
{
    public int Id { get; set; }
    public int Value { get; set; }
    public ParameterDto Parameter { get; set; } = new ParameterDto();
    public ProcessDto Process { get; set; } = new ProcessDto();
}

public class CreateProcessParameterDto
{
    public int ProcessId { get; set; }
    public CreateParameterDto Parameter { get; set; } = new CreateParameterDto();
    public int Value { get; set; }
}

