// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.TeamInfo
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Team
{
  public class TeamInfo : RiotGamesObject
  {
    private string type = "com.riotgames.team.TeamInfo";
    private TeamInfo.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("secondsUntilEligibleForDeletion")]
    public double SecondsUntilEligibleForDeletion { get; set; }

    [InternalName("memberStatusString")]
    public string MemberStatusString { get; set; }

    [InternalName("tag")]
    public string Tag { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("memberStatus")]
    public string MemberStatus { get; set; }

    [InternalName("teamId")]
    public TeamId TeamId { get; set; }

    public TeamInfo()
    {
    }

    public TeamInfo(TeamInfo.Callback callback)
    {
      this.callback = callback;
    }

    public TeamInfo(TypedObject result)
    {
      this.SetFields<TeamInfo>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamInfo>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamInfo result);
  }
}
