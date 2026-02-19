namespace TemplateApi.Dto;

public class BasicDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime Date { get; set; }
}
