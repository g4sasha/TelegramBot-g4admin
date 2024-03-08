using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot_g4admin;

internal static class Program
{
	private static void Main()
	{
        var fileName = "token.txt";
        var token = string.Empty;

        Host bot;

        if (!System.IO.File.Exists(fileName))
        {
            Console.WriteLine($"Не найден файл с токеном!\n1. Создайте {fileName} в папке проекта\n2.Вставьте туда свой токен Telegram бота без пробелов и лишних строк (вы можете создать его с помощью https://t.me/BotFather)\n3. Запустите программу снова");
            return;
        }

        try
        {
            bot = new Host(token);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Не удалось запустить бота. Проверьте токен в {fileName}. Возможно он содержит лишние пробелы или строки!");
            Console.WriteLine($"Ошибка: {e}");
            throw;
        }

		bot.OnMessage += MessageHandler;
		bot.Start();
		Console.ReadKey();
        bot.OnMessage -= MessageHandler;
    }

    private static void MessageHandler(ITelegramBotClient sender, Update signal)
    {
		var msg = signal.Message;
        Console.WriteLine($"Обнаружено сообщение: {msg?.Text ?? "[не текст]"}");

        switch(msg?.Text?.ToLower())
        {
            case "ping":
                sender.SendTextMessageAsync(msg.Chat.Id, "Pong!");
                break;
        }
    }
}
