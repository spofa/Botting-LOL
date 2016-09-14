// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Leagues.Pojo.LeagueListDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Leagues.Pojo
{
  public class LeagueListDTO : RiotGamesObject
  {
    private string type = "com.riotgames.leagues.pojo.LeagueListDTO";
    private LeagueListDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("queue")]
    public string Queue { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("tier")]
    public string Tier { get; set; }

    [InternalName("requestorsRank")]
    public string RequestorsRank { get; set; }

    [InternalName("entries")]
    public List<LeagueItemDTO> Entries { get; set; }

    [InternalName("requestorsName")]
    public string RequestorsName { get; set; }

    public LeagueListDTO()
    {
    }

    public LeagueListDTO(LeagueListDTO.Callback callback)
    {
      this.callback = callback;
    }

    public LeagueListDTO(TypedObject result)
    {
      this.SetFields<LeagueListDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<LeagueListDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(LeagueListDTO result);
  }
}
