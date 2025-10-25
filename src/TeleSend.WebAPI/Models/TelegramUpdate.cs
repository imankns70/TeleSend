using TeleSend.WebAPI.Models;

namespace TeleSend.WebAPI.Services
{
    public class TelegramUpdate
    {
        public long UpdateId { get; set; }
        public TelegramMessage Message { get; set; }
    }
}
