using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot_g4admin;

internal class Host(string token)
{
    private readonly TelegramBotClient bot = new TelegramBotClient(token);
    public Action<ITelegramBotClient, Update>? OnMessage;

    public void Start()
    {
        bot.StartReceiving(UpdateHandler, ErrorHandler);
        Console.WriteLine("Бот готов к работе!");
    }

    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine($"Возникла ошибка! Ошибка: {exception.Message}");
        await Task.CompletedTask;
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        OnMessage?.Invoke(client, update);
        await Task.CompletedTask;
    }
}