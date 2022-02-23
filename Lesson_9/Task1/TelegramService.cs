using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Lesson_9
{
    public class TelegramService
    {
        public readonly TelegramBotClient _botClient;
        private string _token = "///";

        public TelegramService()
        {
            _botClient = new TelegramBotClient(_token);
        }
        
        /// <summary>
        /// Обработчик события бота OnMessage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string text = $"{e.Message.Date.AddHours(3)} {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            Console.WriteLine($"{text}\nTypeMessage: {e.Message.Type}");

            switch (e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Document:
                    DownLoadDoc(e.Message.Document.FileId, e.Message.Document.FileName);
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Voice:
                    DownLoadVoice(e.Message.Voice.FileId, e.Message.Voice.FileUniqueId);
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Photo:
                    int a = e.Message.Photo.Length;
                    DownLoadPhoto(e.Message.Photo[a - 1].FileId, e.Message.Photo[a - 1].FileUniqueId);

                    break;
                default: goto reply;

            }

            Console.WriteLine("Файл сохранен");
            _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Файл получен");

        reply:
            if (e.Message.Text == "/start")
            {
                _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать в чат! в \"Меню\" можно ознакомиться с списком комманд");

            }

            if (e.Message.Text == "/show_files")
            {

                List<FileInfo> files = new List<FileInfo>(new DirectoryInfo("Download").GetFiles());
                if (files.Count == 0)
                {
                    _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Нет файлов для загрузки");
                    return;
                }
                InlineKeyboardButton[][] buttonArr = new InlineKeyboardButton[files.Count][];

                for (int i = 0; i < files.Count; i++)
                {
                    InlineKeyboardButton button = new InlineKeyboardButton() { Text = files[i].Name, CallbackData = i.ToString() };
                    buttonArr[i] = new[] { button };
                    Console.WriteLine(files[i].Name);
                }
                InlineKeyboardMarkup buttons = new InlineKeyboardMarkup(buttonArr);
                _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Кликните файл для загрузки", replyMarkup: buttons);
                return;
            }


            if (e.Message.Text == null) return;

            var messageText = e.Message.Text;


            _botClient.SendTextMessageAsync(e.Message.Chat.Id, $"{messageText}");
        }


        /// <summary>
        /// Обработчик события бота OnCallbackQuery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            Console.WriteLine($"{e.CallbackQuery.Message.Date.AddHours(3)} User nick: {e.CallbackQuery.Message.Chat.FirstName} User ID: {e.CallbackQuery.Message.Chat.Id} Query Id: {e.CallbackQuery.Id}");

            List<string> files = new List<string>(Directory.GetFiles("Download"));

            Upload(files[int.Parse(e.CallbackQuery.Data)], e.CallbackQuery.Message.Chat.Id);

        }

        private async void Upload(string fileName, long chatID)
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

        private async void DownLoadDoc(string fileId, string path)
        {
            var file = await _botClient.GetFileAsync(fileId);

            FileStream fs = new FileStream(@"Download\_" + path, FileMode.Create);
            await _botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }
        private async void DownLoadVoice(string fileId, string path)
        {
            var file = await _botClient.GetFileAsync(fileId);
            FileStream fs = new FileStream(@"Download\_" + path + ".mp3", FileMode.Create);
            await _botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }
        public async void DownLoadPhoto(string fileId, string path)
        {
            var file = await _botClient.GetFileAsync(fileId);
            FileStream fs = new FileStream(@"Download\_" + path + ".jpg", FileMode.Create);
            await _botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }
    }
}
