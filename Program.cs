using PRTelegramBot.Builders;
using PRTelegramBot.Models.EventsArgs;
using Microsoft.Extensions.Logging;
using SatelliteBot.Commands;

Console.WriteLine("Запуск бота спутников планет...");

// Замените "YOUR_BOT_TOKEN" на токен вашего бота от BotFather
string botToken = "YOUR_BOT_TOKEN";

var telegram = new PRBotBuilder(botToken)
    .SetBotId(0)
    .SetClearUpdatesOnStart(true)
    .SetLoggerFactory(Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole()))
    .Build();

// Подписка на логи с ошибками
telegram.Events.OnErrorLog += Events_OnErrorLog;

// Запуск работы бота
await telegram.StartAsync();

Console.WriteLine("Бот запущен! Нажмите Enter для остановки.");
Console.ReadLine();

async Task Events_OnErrorLog(ErrorLogEventArgs arg)
{
    Console.WriteLine($"Ошибка: {arg.Exception.Message}");
}
