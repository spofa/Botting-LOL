// Decompiled with JetBrains decompiler
// Type: ezBot.Utils.Generator
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Threading;

namespace ezBot.Utils
{
  public static class Generator
  {
    public static Random r { get; private set; }

    static Generator()
    {
      Generator.r = new Random();
    }

    public static int CreateRandom(int min, int max)
    {
      return Generator.r.Next(min, max);
    }

    public static void CreateRandomThread(int min, int max)
    {
      Thread.Sleep(Generator.r.Next(min, max));
    }
  }
}
