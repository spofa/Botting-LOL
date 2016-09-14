// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Leagues.Client.Dto.SummonerLeagueItemsDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Leagues.Pojo;
using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Leagues.Client.Dto
{
  public class SummonerLeagueItemsDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.leagues.client.dto.SummonerLeagueItemsDTO";
    private SummonerLeagueItemsDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("summonerLeagues")]
    public List<LeagueItemDTO> SummonerLeagues { get; set; }

    public SummonerLeagueItemsDTO()
    {
    }

    public SummonerLeagueItemsDTO(SummonerLeagueItemsDTO.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerLeagueItemsDTO(TypedObject result)
    {
      this.SetFields<SummonerLeagueItemsDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerLeagueItemsDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerLeagueItemsDTO result);
  }
}
