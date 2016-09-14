// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.RecentGames
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class RecentGames : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.RecentGames";
    private RecentGames.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("recentGamesJson")]
    public object RecentGamesJson { get; set; }

    [InternalName("playerGameStatsMap")]
    public TypedObject PlayerGameStatsMap { get; set; }

    [InternalName("gameStatistics")]
    public List<PlayerGameStats> GameStatistics { get; set; }

    [InternalName("userId")]
    public double UserId { get; set; }

    public RecentGames()
    {
    }

    public RecentGames(RecentGames.Callback callback)
    {
      this.callback = callback;
    }

    public RecentGames(TypedObject result)
    {
      this.SetFields<RecentGames>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<RecentGames>(this, result);
      this.callback(this);
    }

    public delegate void Callback(RecentGames result);
  }
}
