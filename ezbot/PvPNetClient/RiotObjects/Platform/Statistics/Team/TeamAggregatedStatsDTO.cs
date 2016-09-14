// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.Team.TeamAggregatedStatsDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Team;
using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics.Team
{
  public class TeamAggregatedStatsDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.team.TeamAggregatedStatsDTO";
    private TeamAggregatedStatsDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("queueType")]
    public string QueueType { get; set; }

    [InternalName("serializedToJson")]
    public string SerializedToJson { get; set; }

    [InternalName("playerAggregatedStatsList")]
    public List<TeamPlayerAggregatedStatsDTO> PlayerAggregatedStatsList { get; set; }

    [InternalName("teamId")]
    public TeamId TeamId { get; set; }

    public TeamAggregatedStatsDTO()
    {
    }

    public TeamAggregatedStatsDTO(TeamAggregatedStatsDTO.Callback callback)
    {
      this.callback = callback;
    }

    public TeamAggregatedStatsDTO(TypedObject result)
    {
      this.SetFields<TeamAggregatedStatsDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamAggregatedStatsDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamAggregatedStatsDTO result);
  }
}
