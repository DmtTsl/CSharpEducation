using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;


namespace Lesson_9
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Directory.CreateDirectory("Download");

            var ts = new TelegramService();

            ts._botClient.OnCallbackQuery += ts.CallbackQuery;
            ts._botClient.OnMessage += ts.MessageListener;

            ts._botClient.StartReceiving();
            Console.ReadKey();
            

        }

        
        

       



        
    }
}
