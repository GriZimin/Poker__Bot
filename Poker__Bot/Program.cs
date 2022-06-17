using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;

namespace TelegramBotExperiments
{

    class PudgikBot
    {
        static ITelegramBotClient bot = new TelegramBotClient("5324566614:AAHFxvbBLfV1rx-VCtV9mXJla8BHj0yd0zU");
        public static List<Card> allCards = new List<Card>();

        public static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
           
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                // Команда запуска 
                if (message.Text.ToLower() == "/start")
                {
                    await client.SendTextMessageAsync(message.Chat, "Приветствую тебя на величайшем казино (на может я немного преувеличиваю) этой Телеграм группы.\n Здесь вы сможете играть на фишки в покер не внося реальных денег (ну от пожертвования я бы не отказался). Пишите /poker чтобы начать играть.\n Повеселитесь!\n Бот на стадии разработки.");
                    return;
                }
                if (message.Text.ToLower() == "/poker")
                {
                    
                    int playersNum = 0;
                    int i = 0;
                    await client.SendTextMessageAsync(message.Chat, "Я рад что ты согласился. Но я бы хотел узнать кто будет играть. Каждый играющий участник группы должен написать \"Я в игре\". Когда все будут в игре, напишите пожалуйста \"end\" и я начну игру.");
                    string s = null;
                    while (s == null)
                    {
                        s = update.CallbackQuery.Data;
                    }
                    Console.WriteLine(s);
                        if (s == "Я в игре")
                        {
                            playersNum++;
                            await client.SendTextMessageAsync(message.Chat, $"Хорошо, {message.AuthorSignature}, я тебя записал");
                            i++;
                        }
                        if (s == "end")
                        {
                            await client.SendTextMessageAsync(message.Chat, $"Хорошо, всего {playersNum} челокек. Я начинаю игру.");
                        }
                        if (s == "/stop") return;
                    
                    await client.SendTextMessageAsync(message.Chat, $"Конец.");
                    return;
                }
                

                await client.SendTextMessageAsync(message.Chat, "Простите, команда не распознана.");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот" + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
public class Card
{
    public Card(string type, string suit)
    {

    }
}