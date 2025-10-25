namespace TeleSend.WebAPI.Models
{
    public class TelegramMessage
    {
        public long MessageId { get; set; }
        public TelegramUser From { get; set; }
        public long ChatId { get; set; } // شناسه گروه
        public string Text { get; set; }
        public TelegramPhoto[] Photo { get; set; } // اگر عکس باشه
        public TelegramDocument Document { get; set; } // اگر فایل باشه
    }
}
