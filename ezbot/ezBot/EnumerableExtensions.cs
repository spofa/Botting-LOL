// Decompiled with JetBrains decompiler
// Type: ezBot.EnumerableExtensions
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace ezBot
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
      return source.Shuffle<T>(new Random());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (rng == null)
        throw new ArgumentNullException("rng");
      return source.ShuffleIterator<T>(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
    {
      List<T> buffer = source.ToList<T>();
      for (int i = 0; i < buffer.Count; ++i)
      {
        int j = rng.Next(i, buffer.Count);
        yield return buffer[j];
        buffer[j] = buffer[i];
      }
    }
  }
}
