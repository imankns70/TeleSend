using TeleSend.WebAPI.Services;
using TeleSend.WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);


// خواندن تنظیمات تلگرام از appsettings.json
var telegramConfig = builder.Configuration.GetSection("Telegram")
                        .Get<TelegramConfig>();

// ثبت factory
builder.Services.AddSingleton<TelegramServiceFactory>();

// ثبت TelegramService از طریق factory
builder.Services.AddSingleton<TelegramService>(sp =>
{
    var factory = sp.GetRequiredService<TelegramServiceFactory>();
    return factory.Create();
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
