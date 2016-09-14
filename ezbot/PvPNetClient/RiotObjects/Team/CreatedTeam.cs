// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.CreatedTeam
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Team
{
  public class CreatedTeam : RiotGamesObject
  {
    private string type = "com.riotgames.team.CreatedTeam";
    private CreatedTeam.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("timeStamp")]
    public double TimeStamp { get; set; }

    [InternalName("teamId")]
    public TeamId TeamId { get; set; }

    public CreatedTeam()
    {
    }

    public CreatedTeam(CreatedTeam.Callback callback)
    {
      this.callback = callback;
    }

    public CreatedTeam(TypedObject result)
    {
      this.SetFields<CreatedTeam>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<CreatedTeam>(this, result);
      this.callback(this);
    }

    public delegate void Callback(CreatedTeam result);
  }
}
