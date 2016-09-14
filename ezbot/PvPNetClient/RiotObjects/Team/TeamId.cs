// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.TeamId
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Team
{
  public class TeamId : RiotGamesObject
  {
    private string type = "com.riotgames.team.TeamId";
    private TeamId.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("fullId")]
    public string FullId { get; set; }

    public TeamId()
    {
    }

    public TeamId(TeamId.Callback callback)
    {
      this.callback = callback;
    }

    public TeamId(TypedObject result)
    {
      this.SetFields<TeamId>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamId>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamId result);
  }
}
