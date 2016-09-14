// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.Champion.ChampionDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Catalog.Champion
{
  public class ChampionDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.champion.ChampionDTO";
    private ChampionDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("searchTags")]
    public string[] SearchTags { get; set; }

    [InternalName("ownedByYourTeam")]
    public bool OwnedByYourTeam { get; set; }

    [InternalName("botEnabled")]
    public bool BotEnabled { get; set; }

    [InternalName("banned")]
    public bool Banned { get; set; }

    [InternalName("skinName")]
    public string SkinName { get; set; }

    [InternalName("displayName")]
    public string DisplayName { get; set; }

    [InternalName("championData")]
    public TypedObject ChampionData { get; set; }

    [InternalName("owned")]
    public bool Owned { get; set; }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    [InternalName("freeToPlayReward")]
    public bool FreeToPlayReward { get; set; }

    [InternalName("freeToPlay")]
    public bool FreeToPlay { get; set; }

    [InternalName("ownedByEnemyTeam")]
    public bool OwnedByEnemyTeam { get; set; }

    [InternalName("active")]
    public bool Active { get; set; }

    [InternalName("championSkins")]
    public List<ChampionSkinDTO> ChampionSkins { get; set; }

    [InternalName("description")]
    public string Description { get; set; }

    [InternalName("winCountRemaining")]
    public int WinCountRemaining { get; set; }

    [InternalName("purchaseDate")]
    public double PurchaseDate { get; set; }

    [InternalName("endDate")]
    public int EndDate { get; set; }

    public ChampionDTO()
    {
    }

    public ChampionDTO(ChampionDTO.Callback callback)
    {
      this.callback = callback;
    }

    public ChampionDTO(TypedObject result)
    {
      this.SetFields<ChampionDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ChampionDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ChampionDTO result);
  }
}
