using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeepMeRunning
{
    class Program
    {
        private static Process[] pl;
        private static Process bot, eloBuddy;


        static void Main(string[] args)
        {
            pl = Process.GetProcessesByName("ezBot");
            if (pl.Length < 0)
                bot = pl[0];
            else
                bot = Process.Start("ezBot.exe");

            bot.Exited += Bot_Exited;
            bot.EnableRaisingEvents = true;

            pl = Process.GetProcessesByName("EloBuddy.Loader");
            if (pl.Length > 0)
                eloBuddy = pl[0];
            else
                eloBuddy = Process.Start(@"C:\Program Files (x86)\EloBuddy\EloBuddy.Loader.exe");
            eloBuddy.Exited += Restart_EloBuddy;
            eloBuddy.EnableRaisingEvents = true;

            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Console.Title = "ezBot: " + DateTime.Now;
                    Thread.Sleep(1000);
                    if (!eloBuddy.Responding)
                    {
                        Thread.Sleep(5000);
                        if (!eloBuddy.Responding)
                            Restart_EloBuddy(null, EventArgs.Empty);
                    }
                }
            })).Start();


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You may AFK now :=)\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ReadLine();
        }

        private static void Restart_EloBuddy(object p, EventArgs empty)
        {
            eloBuddy.Exited -= Restart_EloBuddy;
            if (eloBuddy.Responding)
                Process.Start("taskkill /F /IM EloBuddy.Loader.exe");
            eloBuddy = Process.Start(@"C:\Program Files (x86)\EloBuddy\EloBuddy.Loader.exe");
            eloBuddy.Exited += Restart_EloBuddy;
            eloBuddy.EnableRaisingEvents = true;
            Console.WriteLine("EloBuddy Restarted");
        }

        private static void Bot_Exited(object sender, EventArgs e)
        {
            bot.Exited -= Bot_Exited;
            bot = Process.Start("ezBot.exe");
            bot.Exited += Bot_Exited;
            bot.EnableRaisingEvents = true;
            Console.WriteLine("ezBot Restarted!");
        }
    }
}
