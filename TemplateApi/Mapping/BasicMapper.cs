namespace TemplateApi.Mapping;

using TemplateApi.Dto;
using TemplateApi.Models;
using TemplateApi.Parameters;

public static class BasicMapper
{
    public static BasicDto ToDto(BasicModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new BasicDto
        {
            Id = model.Id,
            Name = model.Name,
            Location = model.Location,
            Date = model.Date
        };
    }

    public static BasicModel From(CreateBasicParams p)
    {
        ArgumentNullException.ThrowIfNull(p);

        return new BasicModel
        {
            Name = p.Name,
            Location = p.Location,
            Date = p.Date
        };
    }

    public static void Apply(UpdateBasicParams p, BasicModel model)
    {
        ArgumentNullException.ThrowIfNull(p);
        ArgumentNullException.ThrowIfNull(model);

        model.Name = p.Name;
        model.Location = p.Location;
        model.Date = p.Date;
    }
}
