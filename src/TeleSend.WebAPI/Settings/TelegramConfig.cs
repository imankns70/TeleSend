namespace TeleSend.WebAPI.Settings
{
    public class TelegramConfig
    {
        public string BotToken { get; set; }
        public List<GroupChannelMapping> GroupToChannel { get; set; }
    }
}
