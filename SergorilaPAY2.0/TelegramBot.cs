using Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SergorilaPAY2._0
{
    public class TelegramBot
    {
        private readonly TelegramBotClient _tgBot;
        private readonly IUserDao _userDao;
        private readonly ILogger<TelegramBot> _logger;
        private readonly string _token = "6266328958:AAF42k8X_XVXRE-Y9n4RfHth3tZ-PIEQ_7o";
        
        private string PreviousMessage { get; set; }

        private const string SALT = "AMOGU$AB1GU$SUG0M4";

        public TelegramBot(IUserDao userDao)
        {
            _userDao = userDao;
            _logger = new Logger<TelegramBot>(LoggerFactory.Create(loggerBuilder =>
            {
                loggerBuilder.SetMinimumLevel(LogLevel.Trace).AddConsole();
            }));
            
            _tgBot = new TelegramBotClient(_token);
        }

        public void StartPolling()
        {
            PreviousMessage = "/start";
            _tgBot.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        private async void Update(
            ITelegramBotClient tgClient,
            Update update,
            CancellationToken token)
        {
            var message = update.Message.Text;
            var userId = update.Message.Chat.Username;
            var chatId = update.Message.Chat.Id;
            var rnd = new Random();

            var user = await _userDao.GetUserTGAsync(userId);

            if (user != null)
            {
                switch (message)
                {
                    case "/getPassword":
                        var resultPassword = "";
                        
                        var bytes = new byte[25];
                        rnd.NextBytes(bytes);
                        
                        foreach (var c in bytes)
                        {
                            resultPassword += (char)(c % 26 + 94);
                        }
                        
                        tgClient.SendTextMessageAsync(chatId, resultPassword, cancellationToken: token);

                        user.Password = resultPassword;
                        _userDao.UpdateUserAsync(user);

                        break;
                    default:
                        tgClient.SendTextMessageAsync(chatId, 
                            "Введите /getPassword",
                            cancellationToken: token);
                        break;
                }
            }
            else 
            {
                tgClient.SendTextMessageAsync(chatId, "Вас нет в базе", cancellationToken: token);
            }
        }

        private void Error(
            ITelegramBotClient tgClient,
            Exception ex,
            CancellationToken token)
        {
            _logger.LogError(
                "{ExceptionType}: There's some error. {ExceptionMessage}",
                ex.GetType(),
                ex.Message);
        }
    }
}