using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Lesson_10
{
    public class TelegramService
    {
        
        private readonly TelegramBotClient _botClient;
        private string _token = "5176010751:AAFAjY5qpD4bg2XFdYzyTP4Mzwo8eHWLErM";

        public ObservableCollection<User> users { get; set; }
        public User user { get; set; }
        //public Dictionary<long, string> users { get; set; }
        private MainWindow _mw;
        
        public TelegramService(MainWindow MW)
        {
            _mw = MW;
            _botClient = new TelegramBotClient(_token);
            users = new ObservableCollection<User>();
            user = new User();
            _botClient.OnMessage += MessageListener;
            _botClient.StartReceiving();

        }
        
        /// <summary>
        /// Обработчик события бота OnMessage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            
            _mw.Dispatcher.Invoke(() =>
            {
                int index = 0;
                bool newUser = true;
                Message message = new Message(e.Message.Date, e.Message.Chat.FirstName, e.Message.Text);
                user = new User(e.Message.Chat.Id, e.Message.Chat.FirstName);
                foreach(User u in users)
                {
                    
                    if (u.ID == user.ID)
                    {
                        newUser = false;
                        
                        break;
                    }
                    index++;
                }
                if (newUser)
                {
                    users.Add(user);
                    user.Messages.Add(message);
                    
                }
                else
                {
                    users[index].Messages.Add(message);
                }
                

                
            });



        }
        public void SendMessage(string Text, long Id)
        {
            
            _botClient.SendTextMessageAsync(Id, Text);
        }


        /// <summary>
        /// Обработчик события бота OnCallbackQuery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            Console.WriteLine($"{e.CallbackQuery.Message.Date.AddHours(3)} User nick: {e.CallbackQuery.Message.Chat.FirstName} User ID: {e.CallbackQuery.Message.Chat.Id} Query Id: {e.CallbackQuery.Id}");

            List<string> files = new List<string>(Directory.GetFiles($@"Download\{e.CallbackQuery.Message.Chat.Id}\"));

            UploadFile(files[int.Parse(e.CallbackQuery.Data)], e.CallbackQuery.Message.Chat.Id);

        }

        private async void UploadFile(string fileName, long chatID)
        {

            using (var stream = File.Open(fileName, FileMode.Open))
            {
                string fName = new FileInfo(fileName).Name;
                switch (new FileInfo(fileName).Extension)
                {
                    case ".jpg":
                        await _botClient.SendPhotoAsync(chatID, new InputOnlineFile(stream, fName), "Получай");
                        Console.WriteLine("Файл отправлен");
                        break;
                    case ".mp3":
                        await _botClient.SendAudioAsync(chatID, new InputOnlineFile(stream, fName), "Получай");
                        Console.WriteLine("Файл отправлен");
                        break;
                    default:
                        await _botClient.SendDocumentAsync(chatID, new InputOnlineFile(stream, fName), "Получай");
                        Console.WriteLine("Файл отправлен");
                        break;
                }


            }
        }

        private async void SendTextMessage(long userID, string message, IReplyMarkup replyMarkup = null)
        {
            await _botClient.SendTextMessageAsync(userID, message, replyMarkup: replyMarkup);
        }

        private async void DownLoadDoc(string fileId, string path, long userID)
        {
            try
            {
                var file = await _botClient.GetFileAsync(fileId);
                FileStream fs = new FileStream($@"Download\{userID}\_" + path, FileMode.Create);
                await _botClient.DownloadFileAsync(file.FilePath, fs);
                fs.Close();

                fs.Dispose();
                Console.WriteLine("Файл сохранен");
                SendTextMessage(userID, "Файл получен");
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException e)
            {
                Console.WriteLine(e.Message);
                SendTextMessage(userID, e.Message);
            }
            
            
        }
        private async void DownLoadVoice(string fileId, string path, long userID)
        {
            try 
            { 
            var file = await _botClient.GetFileAsync(fileId);
            FileStream fs = new FileStream($@"Download\{userID}\_" + path + ".mp3", FileMode.Create);
            await _botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
            Console.WriteLine("Файл сохранен");
                SendTextMessage(userID, "Файл получен");
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException e)
            {
                Console.WriteLine(e.Message);
                SendTextMessage(userID, e.Message);
            }
        }
        public async void DownLoadPhoto(string fileId, string path, long userID)
        {
            try
            {
                var file = await _botClient.GetFileAsync(fileId);
                FileStream fs = new FileStream($@"Download\{userID}\_" + path + ".jpg", FileMode.Create);
                await _botClient.DownloadFileAsync(file.FilePath, fs);
                fs.Close();

                fs.Dispose();
                Console.WriteLine("Файл сохранен");
                SendTextMessage(userID, "Файл получен");
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException e)
            {
                Console.WriteLine(e.Message);
                SendTextMessage(userID, e.Message);
            }

            
        }
       
    }
}
