// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.PlayerStatSummary
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class PlayerStatSummary : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.PlayerStatSummary";
    private PlayerStatSummary.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("maxRating")]
    public int MaxRating { get; set; }

    [InternalName("playerStatSummaryTypeString")]
    public string PlayerStatSummaryTypeString { get; set; }

    [InternalName("aggregatedStats")]
    public SummaryAggStats AggregatedStats { get; set; }

    [InternalName("modifyDate")]
    public DateTime ModifyDate { get; set; }

    [InternalName("leaves")]
    public int Leaves { get; set; }

    [InternalName("playerStatSummaryType")]
    public string PlayerStatSummaryType { get; set; }

    [InternalName("userId")]
    public double UserId { get; set; }

    [InternalName("losses")]
    public int Losses { get; set; }

    [InternalName("rating")]
    public int Rating { get; set; }

    [InternalName("aggregatedStatsJson")]
    public object AggregatedStatsJson { get; set; }

    [InternalName("wins")]
    public int Wins { get; set; }

    public PlayerStatSummary()
    {
    }

    public PlayerStatSummary(PlayerStatSummary.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerStatSummary(TypedObject result)
    {
      this.SetFields<PlayerStatSummary>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerStatSummary>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerStatSummary result);
  }
}
