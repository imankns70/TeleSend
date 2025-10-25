using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TeleSend.WebAPI.Models;

namespace TeleSend.WebAPI.Services
{
    public class TelegramService
    {
        private readonly TelegramBotClient _botClient;
        private readonly Dictionary<long, long> _groupToChannelMap;

        public TelegramService(string botToken, Dictionary<long, long> groupToChannelMap)
        {
            _botClient = new TelegramBotClient(botToken);
            _groupToChannelMap = groupToChannelMap;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            if (update?.Message == null) return;

            TelegramUpdate telegramUpdate = ConvertToInternalUpdate(update);
            var chatId = telegramUpdate.Message.ChatId;

            // بررسی اینکه گروه در نگاشت ما هست
            if (!_groupToChannelMap.TryGetValue(chatId, out var targetChannelId))
                return; // گروه نامعتبر، نادیده گرفته می‌شود

            // ارسال متن
            if (!string.IsNullOrEmpty(telegramUpdate.Message.Text))
            {
                await _botClient.SendMessage(
                    chatId: targetChannelId,
                    text: telegramUpdate.Message.Text,
                    parseMode: ParseMode.Html
                );
            }

            // ارسال عکس
            if (telegramUpdate.Message.Photo != null && telegramUpdate.Message.Photo.Any())
            {
                var largestPhoto = telegramUpdate.Message.Photo.OrderByDescending(p => p.Width * p.Height).First();
                await _botClient.SendPhoto(
                    chatId: targetChannelId,
                    photo: largestPhoto.FileId
                );
            }

            // ارسال فایل یا سند
            if (telegramUpdate.Message.Document != null)
            {
                await _botClient.SendDocument(
                    chatId: targetChannelId,
                    document: telegramUpdate.Message.Document.FileId
                );
            }
        }

        private TelegramUpdate ConvertToInternalUpdate(Update update)
        {
            if (update.Message == null) return null;

            return new TelegramUpdate
            {
                UpdateId = update.Id,
                Message = new TelegramMessage
                {
                    MessageId = update.Message.MessageId,
                    ChatId = update.Message.Chat.Id,
                    Text = update.Message.Text,
                    From = new TelegramUser
                    {
                        Id = update.Message.From.Id,
                        FirstName = update.Message.From.FirstName,
                        Username = update.Message.From.Username
                    },
                    Photo = update.Message.Photo?.Select(p => new TelegramPhoto
                    {
                        FileId = p.FileId,
                        Width = p.Width,
                        Height = p.Height
                    }).ToArray(),
                    Document = update.Message.Document == null ? null : new TelegramDocument
                    {
                        FileId = update.Message.Document.FileId,
                        FileName = update.Message.Document.FileName
                    }
                }
            };
        }
    }

}
