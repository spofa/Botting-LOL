// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.AllSummonerData
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Summoner.Masterybook;
using PvPNetClient.RiotObjects.Platform.Summoner.Spellbook;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class AllSummonerData : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.AllSummonerData";
    private AllSummonerData.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("spellBook")]
    public SpellBookDTO SpellBook { get; set; }

    [InternalName("summonerDefaultSpells")]
    public SummonerDefaultSpells SummonerDefaultSpells { get; set; }

    [InternalName("summonerTalentsAndPoints")]
    public SummonerTalentsAndPoints SummonerTalentsAndPoints { get; set; }

    [InternalName("summoner")]
    public PvPNetClient.RiotObjects.Platform.Summoner.Summoner Summoner { get; set; }

    [InternalName("masteryBook")]
    public MasteryBookDTO MasteryBook { get; set; }

    [InternalName("summonerLevelAndPoints")]
    public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

    [InternalName("summonerLevel")]
    public SummonerLevel SummonerLevel { get; set; }

    public AllSummonerData()
    {
    }

    public AllSummonerData(AllSummonerData.Callback callback)
    {
      this.callback = callback;
    }

    public AllSummonerData(TypedObject result)
    {
      this.SetFields<AllSummonerData>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AllSummonerData>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AllSummonerData result);
  }
}
