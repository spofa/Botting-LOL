// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.Stats.TeamStatDetail
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Team.Stats
{
  public class TeamStatDetail : RiotGamesObject
  {
    private string type = "com.riotgames.team.stats.TeamStatDetail";
    private TeamStatDetail.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("maxRating")]
    public int MaxRating { get; set; }

    [InternalName("teamIdString")]
    public string TeamIdString { get; set; }

    [InternalName("seedRating")]
    public int SeedRating { get; set; }

    [InternalName("losses")]
    public int Losses { get; set; }

    [InternalName("rating")]
    public int Rating { get; set; }

    [InternalName("teamStatTypeString")]
    public string TeamStatTypeString { get; set; }

    [InternalName("averageGamesPlayed")]
    public int AverageGamesPlayed { get; set; }

    [InternalName("teamId")]
    public TeamId TeamId { get; set; }

    [InternalName("wins")]
    public int Wins { get; set; }

    [InternalName("teamStatType")]
    public string TeamStatType { get; set; }

    public TeamStatDetail()
    {
    }

    public TeamStatDetail(TeamStatDetail.Callback callback)
    {
      this.callback = callback;
    }

    public TeamStatDetail(TypedObject result)
    {
      this.SetFields<TeamStatDetail>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamStatDetail>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamStatDetail result);
  }
}
