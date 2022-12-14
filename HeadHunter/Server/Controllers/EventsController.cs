using HeadHunter.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventsService _service;

        public EventsController(EventsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _service.GetEventsAsync();
            return Ok(events);
        }
    }
}
