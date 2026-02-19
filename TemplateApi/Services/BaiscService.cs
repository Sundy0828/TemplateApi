namespace TemplateApi.Services;

using Microsoft.AspNetCore.Mvc;
using TemplateApi.Attributes;
using TemplateApi.Domains.Interfaces;
using TemplateApi.Mapping;
using TemplateApi.Models;
using TemplateApi.Paging;
using TemplateApi.Parameters;
using TemplateApi.Utility;

[ApiController]
[Route("[controller]")]
[ApiKeyAuthorization]
public class BasicService(IBasicDomain domain) : ControllerBase
{
    private readonly IBasicDomain _domain = domain;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllBasicParams parameters)
    {
        var result = await _domain.GetAllAsync(parameters);

        var dtoItems = result.Items.Select(BasicMapper.ToDto).ToList();

        return Ok(PagedResultFactory.Create(
            dtoItems,
            result.PageNumber,
            result.PageSize,
            result.TotalItems));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        Guard.AgainstNullOrWhiteSpace(id, nameof(id));

        var basicModel = await _domain.GetByIdAsync(id);

        return Ok(BasicMapper.ToDto(basicModel));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBasicParams parameters)
    {
        Guard.AgainstNull(parameters, nameof(parameters));
        Guard.AgainstNullOrWhiteSpace(parameters.Name, nameof(parameters.Name));
        Guard.AgainstNullOrWhiteSpace(parameters.Location, nameof(parameters.Location));

        var createdModel = await _domain.CreateAsync(BasicMapper.From(parameters));
        return Created($"/basic/{createdModel.Id}", BasicMapper.ToDto(createdModel));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateBasicParams parameters)
    {
        Guard.AgainstNull(parameters, nameof(parameters));
        Guard.AgainstNullOrWhiteSpace(id, nameof(id));
        Guard.AgainstNullOrWhiteSpace(parameters.Name, nameof(parameters.Name));
        Guard.AgainstNullOrWhiteSpace(parameters.Location, nameof(parameters.Location));

        var model = new BasicModel() { Id = id };
        BasicMapper.Apply(parameters, model);
        await _domain.UpdateAsync(id, model);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        Guard.AgainstNullOrWhiteSpace(id, nameof(id));
        await _domain.DeleteAsync(id);

        return NoContent();
    }
}
