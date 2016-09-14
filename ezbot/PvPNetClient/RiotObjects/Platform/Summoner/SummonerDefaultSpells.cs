// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.SummonerDefaultSpells
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class SummonerDefaultSpells : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.SummonerDefaultSpells";
    private SummonerDefaultSpells.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("summonerDefaultSpellsJson")]
    public object SummonerDefaultSpellsJson { get; set; }

    [InternalName("summonerDefaultSpellMap")]
    public TypedObject SummonerDefaultSpellMap { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public SummonerDefaultSpells()
    {
    }

    public SummonerDefaultSpells(SummonerDefaultSpells.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerDefaultSpells(TypedObject result)
    {
      this.SetFields<SummonerDefaultSpells>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerDefaultSpells>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerDefaultSpells result);
  }
}
