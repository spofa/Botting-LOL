// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.Dto.RosterDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Team.Dto
{
  public class RosterDTO : RiotGamesObject
  {
    private string type = "com.riotgames.team.dto.RosterDTO";
    private RosterDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("ownerId")]
    public double OwnerId { get; set; }

    [InternalName("memberList")]
    public List<TeamMemberInfoDTO> MemberList { get; set; }

    public RosterDTO()
    {
    }

    public RosterDTO(RosterDTO.Callback callback)
    {
      this.callback = callback;
    }

    public RosterDTO(TypedObject result)
    {
      this.SetFields<RosterDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<RosterDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(RosterDTO result);
  }
}
