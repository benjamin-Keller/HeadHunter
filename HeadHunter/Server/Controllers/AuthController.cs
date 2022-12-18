using HeadHunter.Server.Services;
using HeadHunter.Shared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] RiotUser riotUser) => Ok(await _service.AuthenticateAsync(riotUser));
}
