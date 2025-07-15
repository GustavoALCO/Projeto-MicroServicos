using Microsoft.AspNetCore.Mvc;

namespace AdsService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("API funcionando!");
}
