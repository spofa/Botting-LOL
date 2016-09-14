// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.ChampionStatInfo
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class ChampionStatInfo : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.ChampionStatInfo";
    private ChampionStatInfo.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("totalGamesPlayed")]
    public int TotalGamesPlayed { get; set; }

    [InternalName("accountId")]
    public double AccountId { get; set; }

    [InternalName("stats")]
    public List<AggregatedStat> Stats { get; set; }

    [InternalName("championId")]
    public double ChampionId { get; set; }

    public ChampionStatInfo()
    {
    }

    public ChampionStatInfo(ChampionStatInfo.Callback callback)
    {
      this.callback = callback;
    }

    public ChampionStatInfo(TypedObject result)
    {
      this.SetFields<ChampionStatInfo>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ChampionStatInfo>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ChampionStatInfo result);
  }
}
