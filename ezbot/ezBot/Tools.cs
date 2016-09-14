// Decompiled with JetBrains decompiler
// Type: ezBot.Tools
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ezBot
{
    internal class Tools
    {
        public static string ezVersion = Application.ProductVersion;

        public static void Log(string text)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\errors.txt";
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter text1 = File.CreateText(path))
                        text1.Write("[" + (object)DateTime.Now + "] " + text + Environment.NewLine);
                }
                else
                {
                    if (!File.Exists(path))
                        return;
                    File.AppendAllText(path, "[" + (object)DateTime.Now + "] " + text + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", (object)ex.ToString());
            }
        }

        public static void ErrorReport(string message)
        {
            File.WriteAllText("error.txt", message);
            ConsoleMessage(message, ConsoleColor.White);
            Thread.Sleep(5000);
        }

        public static void TitleMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + (object)DateTime.Now + "] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message + "\n");
        }

        public static void ConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[" + (object)DateTime.Now + "] ");
            Console.ForegroundColor = color;
            Console.Write(message + "\n");
        }
    }
}