// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.PlayerStats
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class PlayerStats : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.PlayerStats";
    private PlayerStats.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("timeTrackedStats")]
    public List<TimeTrackedStat> TimeTrackedStats { get; set; }

    [InternalName("promoGamesPlayed")]
    public int PromoGamesPlayed { get; set; }

    [InternalName("promoGamesPlayedLastUpdated")]
    public object PromoGamesPlayedLastUpdated { get; set; }

    public PlayerStats()
    {
    }

    public PlayerStats(PlayerStats.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerStats(TypedObject result)
    {
      this.SetFields<PlayerStats>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerStats>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerStats result);
  }
}
