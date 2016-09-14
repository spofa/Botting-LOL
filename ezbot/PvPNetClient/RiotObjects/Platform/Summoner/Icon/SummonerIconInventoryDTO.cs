// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Icon.SummonerIconInventoryDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner.Icon
{
  public class SummonerIconInventoryDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.icon.SummonerIconInventoryDTO";
    private SummonerIconInventoryDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    [InternalName("summonerIcons")]
    public List<PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon> SummonerIcons { get; set; }

    public SummonerIconInventoryDTO()
    {
    }

    public SummonerIconInventoryDTO(SummonerIconInventoryDTO.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerIconInventoryDTO(TypedObject result)
    {
      this.SetFields<SummonerIconInventoryDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerIconInventoryDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerIconInventoryDTO result);
  }
}
