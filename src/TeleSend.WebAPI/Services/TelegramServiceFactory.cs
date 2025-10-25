using TeleSend.WebAPI.Settings;

namespace TeleSend.WebAPI.Services
{
    public class TelegramServiceFactory
    {
        private readonly TelegramConfig _config;
        private readonly Dictionary<long, long> _groupToChannelMap;

        public TelegramServiceFactory(IConfiguration configuration)
        {
            // خواندن تنظیمات تلگرام
            _config = configuration.GetSection("Telegram").Get<TelegramConfig>();
            _groupToChannelMap = _config.GroupToChannel
                                        .ToDictionary(x => x.GroupId, x => x.ChannelId);
        }

        public TelegramService Create()
        {
            return new TelegramService(_config.BotToken, _groupToChannelMap);
        }
    }

}
