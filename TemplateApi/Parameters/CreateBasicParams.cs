namespace TemplateApi.Parameters;

public class CreateBasicParams
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime Date { get; set; }
}
