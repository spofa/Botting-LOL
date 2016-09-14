// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.Stats.TeamStatSummary
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Team.Stats
{
  public class TeamStatSummary : RiotGamesObject
  {
    private string type = "com.riotgames.team.stats.TeamStatSummary";
    private TeamStatSummary.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("teamStatDetails")]
    public List<TeamStatDetail> TeamStatDetails { get; set; }

    [InternalName("teamIdString")]
    public string TeamIdString { get; set; }

    [InternalName("teamId")]
    public TeamId TeamId { get; set; }

    public TeamStatSummary()
    {
    }

    public TeamStatSummary(TeamStatSummary.Callback callback)
    {
      this.callback = callback;
    }

    public TeamStatSummary(TypedObject result)
    {
      this.SetFields<TeamStatSummary>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamStatSummary>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamStatSummary result);
  }
}
