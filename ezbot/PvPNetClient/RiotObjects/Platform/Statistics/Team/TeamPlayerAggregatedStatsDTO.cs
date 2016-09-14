// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.Team.TeamPlayerAggregatedStatsDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Statistics.Team
{
  public class TeamPlayerAggregatedStatsDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.team.TeamPlayerAggregatedStatsDTO";
    private TeamPlayerAggregatedStatsDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("playerId")]
    public double PlayerId { get; set; }

    [InternalName("aggregatedStats")]
    public AggregatedStats AggregatedStats { get; set; }

    public TeamPlayerAggregatedStatsDTO()
    {
    }

    public TeamPlayerAggregatedStatsDTO(TeamPlayerAggregatedStatsDTO.Callback callback)
    {
      this.callback = callback;
    }

    public TeamPlayerAggregatedStatsDTO(TypedObject result)
    {
      this.SetFields<TeamPlayerAggregatedStatsDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamPlayerAggregatedStatsDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamPlayerAggregatedStatsDTO result);
  }
}
