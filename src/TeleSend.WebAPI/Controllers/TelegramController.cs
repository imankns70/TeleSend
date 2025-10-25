using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TeleSend.WebAPI.Services;

namespace TeleSend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly TelegramService _telegramService;

        public TelegramController(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Update update)
        {
            if (update == null) return BadRequest();

            await _telegramService.HandleUpdateAsync(update);

            return Ok();
        }
        [HttpGet]
        public ActionResult GetStatus()
        {
            return Ok("healthy");
        }

    }
}
