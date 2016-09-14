// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.SummonerNames
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects
{
  public class SummonerNames : RiotGamesObject
  {
    private SummonerNames.Callback callback;

    public SummonerNames(SummonerNames.Callback callback)
    {
      this.callback = callback;
    }

    public override void DoCallback(TypedObject result)
    {
      this.callback(result.GetArray("array"));
    }

    public delegate void Callback(object[] result);
  }
}
