// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.AggregatedStats
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class AggregatedStats : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.AggregatedStats";
    private AggregatedStats.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("lifetimeStatistics")]
    public List<AggregatedStat> LifetimeStatistics { get; set; }

    [InternalName("modifyDate")]
    public object ModifyDate { get; set; }

    [InternalName("key")]
    public AggregatedStatsKey Key { get; set; }

    [InternalName("aggregatedStatsJson")]
    public string AggregatedStatsJson { get; set; }

    public AggregatedStats()
    {
    }

    public AggregatedStats(AggregatedStats.Callback callback)
    {
      this.callback = callback;
    }

    public AggregatedStats(TypedObject result)
    {
      this.SetFields<AggregatedStats>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AggregatedStats>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AggregatedStats result);
  }
}
