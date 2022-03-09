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


       
       
    }
}
