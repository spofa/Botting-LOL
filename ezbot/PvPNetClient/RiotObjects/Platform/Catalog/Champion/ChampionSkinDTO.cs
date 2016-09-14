// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.Champion.ChampionSkinDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Catalog.Champion
{
  public class ChampionSkinDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.champion.ChampionSkinDTO";
    private ChampionSkinDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    [InternalName("skinId")]
    public int SkinId { get; set; }

    [InternalName("freeToPlayReward")]
    public bool FreeToPlayReward { get; set; }

    [InternalName("stillObtainable")]
    public bool StillObtainable { get; set; }

    [InternalName("lastSelected")]
    public bool LastSelected { get; set; }

    [InternalName("skinIndex")]
    public int SkinIndex { get; set; }

    [InternalName("owned")]
    public bool Owned { get; set; }

    [InternalName("winCountRemaining")]
    public int WinCountRemaining { get; set; }

    [InternalName("purchaseDate")]
    public int PurchaseDate { get; set; }

    [InternalName("endDate")]
    public int EndDate { get; set; }

    public ChampionSkinDTO()
    {
    }

    public ChampionSkinDTO(ChampionSkinDTO.Callback callback)
    {
      this.callback = callback;
    }

    public ChampionSkinDTO(TypedObject result)
    {
      this.SetFields<ChampionSkinDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ChampionSkinDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ChampionSkinDTO result);
  }
}
