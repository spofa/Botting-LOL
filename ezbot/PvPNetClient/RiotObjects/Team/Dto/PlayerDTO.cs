// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Team.Dto.PlayerDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Team.Dto
{
  public class PlayerDTO : RiotGamesObject
  {
    private string type = "com.riotgames.team.dto.PlayerDTO";
    private PlayerDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("playerId")]
    public double PlayerId { get; set; }

    [InternalName("teamsSummary")]
    public List<object> TeamsSummary { get; set; }

    [InternalName("createdTeams")]
    public List<object> CreatedTeams { get; set; }

    [InternalName("playerTeams")]
    public List<object> PlayerTeams { get; set; }

    public PlayerDTO()
    {
    }

    public PlayerDTO(PlayerDTO.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerDTO(TypedObject result)
    {
      this.SetFields<PlayerDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerDTO result);
  }
}
