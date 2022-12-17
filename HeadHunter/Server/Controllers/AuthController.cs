using HeadHunter.Server.Services;
using HeadHunter.Shared.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Authenticate([FromForm] RiotUser riotUser)
        {
            var userInfo = await _service.AuthenticateAsync(riotUser);
            return Ok(userInfo);
        }
    }
}
