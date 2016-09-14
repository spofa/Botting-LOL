// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Runes.SummonerRune
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Catalog.Runes;
using System;

namespace PvPNetClient.RiotObjects.Platform.Summoner.Runes
{
  public class SummonerRune : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.runes.SummonerRune";
    private SummonerRune.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("purchased")]
    public DateTime Purchased { get; set; }

    [InternalName("purchaseDate")]
    public DateTime PurchaseDate { get; set; }

    [InternalName("runeId")]
    public int RuneId { get; set; }

    [InternalName("quantity")]
    public int Quantity { get; set; }

    [InternalName("rune")]
    public Rune Rune { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public SummonerRune()
    {
    }

    public SummonerRune(SummonerRune.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerRune(TypedObject result)
    {
      this.SetFields<SummonerRune>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerRune>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerRune result);
  }
}
