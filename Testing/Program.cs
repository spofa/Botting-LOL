
using System;
using System.Diagnostics;
using System.Threading;

namespace Testing
{
    class Program
    {
        static Process eloProcess, ezBotProcess;
        static void Main(string[] args)
        {
            Process[] pl;
            pl = Process.GetProcessesByName("EloBuddy.Loader");
            if (pl.Length > 0)
                eloProcess = pl[0];
            else
                eloProcess = Process.Start(@"C:\Program Files (x86)\EloBuddy\EloBuddy.Loader.exe");
            eloProcess.EnableRaisingEvents = true;
            eloProcess.Exited += EloProcess_Exited;

            pl = Process.GetProcessesByName("ezBot");
            if (pl.Length > 0)
                ezBotProcess = pl[0];
            else
                ezBotProcess = Process.Start(@"ezBot.exe");
            ezBotProcess.EnableRaisingEvents = true;
            ezBotProcess.Exited += EzBotProcess_Exited;

            while (true)
            {
                Thread.Sleep(100); //Keep the window open
            }
        }

        private static void EloProcess_Exited(object sender, EventArgs e)
        {
            eloProcess.Exited -= EloProcess_Exited;
            eloProcess = Process.Start(@"C:\Program Files (x86)\EloBuddy\EloBuddy.Loader.exe");
            eloProcess.EnableRaisingEvents = true;
            eloProcess.Exited += EloProcess_Exited;
            Console.WriteLine("Restarting EloBuddy.Loader");
        }

        private static void EzBotProcess_Exited(object sender, EventArgs e)
        {
            eloProcess.Exited -= EloProcess_Exited;
            eloProcess = Process.Start(@"ezBot.exe");
            eloProcess.EnableRaisingEvents = true;
            eloProcess.Exited += EloProcess_Exited;
            Console.WriteLine("Restarting ezBot");
        }
    }
}
