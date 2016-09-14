// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.PlayerLifetimeStats
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class PlayerLifetimeStats : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.PlayerLifetimeStats";
    private PlayerLifetimeStats.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("playerStatSummaries")]
    public PlayerStatSummaries PlayerStatSummaries { get; set; }

    [InternalName("leaverPenaltyStats")]
    public LeaverPenaltyStats LeaverPenaltyStats { get; set; }

    [InternalName("previousFirstWinOfDay")]
    public DateTime PreviousFirstWinOfDay { get; set; }

    [InternalName("userId")]
    public double UserId { get; set; }

    [InternalName("dodgeStreak")]
    public int DodgeStreak { get; set; }

    [InternalName("dodgePenaltyDate")]
    public object DodgePenaltyDate { get; set; }

    [InternalName("playerStatsJson")]
    public object PlayerStatsJson { get; set; }

    [InternalName("playerStats")]
    public PlayerStats PlayerStats { get; set; }

    public PlayerLifetimeStats()
    {
    }

    public PlayerLifetimeStats(PlayerLifetimeStats.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerLifetimeStats(TypedObject result)
    {
      this.SetFields<PlayerLifetimeStats>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerLifetimeStats>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerLifetimeStats result);
  }
}
