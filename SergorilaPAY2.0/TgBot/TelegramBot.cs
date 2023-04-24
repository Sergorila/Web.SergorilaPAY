using System.Security.Cryptography;
using System.Text;
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

                        RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider();
                        byte[] tokenBuffer = new byte[15];
                        cryptRNG.GetBytes(tokenBuffer);
                        var resultPassword = Convert.ToBase64String(tokenBuffer);

                        tgClient.SendTextMessageAsync(
                            chatId, 
                            $"{userId}, Ваш пароль:", 
                            cancellationToken: token);
                        
                        tgClient.SendTextMessageAsync(
                            chatId, 
                            resultPassword, 
                            cancellationToken: token);

                        user.Password = resultPassword;
                        _userDao.UpdateUserAsync(user);

                        break;
                    default:
                        tgClient.SendTextMessageAsync(
                            chatId, 
                            "Введите /getPassword",
                            cancellationToken: token);
                        
                        break;
                }
            }
            else 
            {
                tgClient.SendTextMessageAsync(chatId, 
                    "Вас нет в базе, пожалуйста, пройдите регистрацию", 
                    cancellationToken: token);
            }
        }

        private void Error(
            ITelegramBotClient tgClient,
            Exception ex,
            CancellationToken token)
        {
            _logger.LogError(
                "{ExceptionType}: Ошиб ОЧКА. {ExceptionMessage}",
                ex.GetType(),
                ex.Message);
        }
    }
}