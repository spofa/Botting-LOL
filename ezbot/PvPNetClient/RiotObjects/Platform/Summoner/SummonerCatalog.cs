// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.SummonerCatalog
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class SummonerCatalog : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.SummonerCatalog";
    private SummonerCatalog.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("items")]
    public object Items { get; set; }

    [InternalName("talentTree")]
    public List<TalentGroup> TalentTree { get; set; }

    [InternalName("spellBookConfig")]
    public List<RuneSlot> SpellBookConfig { get; set; }

    public SummonerCatalog()
    {
    }

    public SummonerCatalog(SummonerCatalog.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerCatalog(TypedObject result)
    {
      this.SetFields<SummonerCatalog>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerCatalog>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerCatalog result);
  }
}
