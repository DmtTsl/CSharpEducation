using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread music = new Thread(PlayMusic);
            music.IsBackground = true;
            music.Start();
            PrintAsync(1000, "PA1");
            PrintAsync(2000,"PA2");
            Print(1500,"Main");
            void Print(int milliseconds, string description)
            {
                for (int i = 0; i <= 20; i++)
                {
                    Console.WriteLine($"{description}: Note # {i}; Thread #{Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(milliseconds);
                }

            }
            async void PrintAsync(int milliseconds, string description)
            {
                await Task.Run(()=>Print(milliseconds, description));
            }
            void PlayMusic()
            {
               SoundPlayer player = new SoundPlayer(Properties.Resources._8_Get_Lucky___88_24__);
               player.Play();
            }
            Console.ReadKey();
        }
        
    }
}
