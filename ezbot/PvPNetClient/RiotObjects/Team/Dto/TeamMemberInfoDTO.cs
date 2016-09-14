// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.Dto.TeamMemberInfoDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Team.Dto
{
  public class TeamMemberInfoDTO : RiotGamesObject
  {
    private string type = "com.riotgames.team.dto.TeamMemberInfoDTO";
    private TeamMemberInfoDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("joinDate")]
    public DateTime JoinDate { get; set; }

    [InternalName("playerName")]
    public string PlayerName { get; set; }

    [InternalName("inviteDate")]
    public DateTime InviteDate { get; set; }

    [InternalName("status")]
    public string Status { get; set; }

    [InternalName("playerId")]
    public double PlayerId { get; set; }

    public TeamMemberInfoDTO()
    {
    }

    public TeamMemberInfoDTO(TeamMemberInfoDTO.Callback callback)
    {
      this.callback = callback;
    }

    public TeamMemberInfoDTO(TypedObject result)
    {
      this.SetFields<TeamMemberInfoDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TeamMemberInfoDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TeamMemberInfoDTO result);
  }
}
