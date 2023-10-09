using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("Draft")]
public class DraftController : ControllerBase
{

    public DraftController()
    {
    }

    [HttpGet]
    [Route("Version")]
    public string Get()
    {
        return "Thing";
    }
}

