namespace TemplateApi.Services;

using Microsoft.AspNetCore.Mvc;
using TemplateApi.Attributes;

[ApiController]
[Route("[controller]")]
[ApiKeyAuthorization]
public class HealthService : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealth() => Ok("Health is good!");
}
