using Newtonsoft.Json;
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
        private string _token = "//";

        public ObservableCollection<User> users { get; set; }

        public User user { get; set; }
       
        private MainWindow _mw;

        
        public TelegramService(MainWindow MW)
        {
            _mw = MW;
            _botClient = new TelegramBotClient(_token);
            users = new ObservableCollection<User>();
            
            ///Проверяем есть ли сохраненная история сообщений
            if (File.Exists("chat.json"))
            {
                string json = File.ReadAllText("chat.json");
                users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            }
            
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
                Message message = new Message(e.Message.Date.ToLocalTime(), e.Message.Chat.FirstName, e.Message.Text, e.Message.Chat.Id);
                user = new User(e.Message.Chat.Id, e.Message.Chat.FirstName);
                
                ///Проверяем есть ли пользователь в коллекции. Если нет, то добавляем
                if (ContainUser(users, user, out int index))
                {
                    users[index].Messages.Add(message);
                }
                else
                {                    
                    users.Add(user);
                    users[index].Messages.Add(message); 
                    
                }
                 
            });
        }

        /// <summary>
        /// отправка сообщения пользователю
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Id"></param>
        public void SendMessage(string Text, long Id)
        {            
            _botClient.SendTextMessageAsync(Id, Text);
        }

        /// <summary>
        /// Сохранение истории сообщений
        /// </summary>
        public void SaveMessageHistory()
        {
            File.WriteAllText("chat.json", JsonConvert.SerializeObject(users));
        }
        
        /// <summary>
        /// Проверяет есть ли пользователь в коллекции пользователей
        /// </summary>
        /// <param name="users">коллекция</param>
        /// <param name="user">экземпляр пользователя от которого пришло сообщение</param>
        /// <param name="index">индекс пользователя в коллекции, либо индекс добавления нового пользователя</param>
        /// <returns></returns>
        private bool ContainUser(ObservableCollection<User> users, User user, out int index)
        {
            index = 0;
            foreach(User u in users)
            {
                if(u.ID == user.ID)
                {
                    return true;
                }
                index++;
            }
            return false;
        }
       
    }
}
