// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.SummaryAggStats
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class SummaryAggStats : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.SummaryAggStats";
    private SummaryAggStats.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("statsJson")]
    public object StatsJson { get; set; }

    [InternalName("stats")]
    public List<SummaryAggStat> Stats { get; set; }

    public SummaryAggStats()
    {
    }

    public SummaryAggStats(SummaryAggStats.Callback callback)
    {
      this.callback = callback;
    }

    public SummaryAggStats(TypedObject result)
    {
      this.SetFields<SummaryAggStats>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummaryAggStats>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummaryAggStats result);
  }
}
