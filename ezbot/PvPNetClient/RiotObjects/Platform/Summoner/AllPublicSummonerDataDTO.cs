// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.AllPublicSummonerDataDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Summoner.Spellbook;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class AllPublicSummonerDataDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.AllPublicSummonerDataDTO";
    private AllPublicSummonerDataDTO.Callback callback;

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
    public BasePublicSummonerDTO Summoner { get; set; }

    [InternalName("summonerLevelAndPoints")]
    public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

    [InternalName("summonerLevel")]
    public SummonerLevel SummonerLevel { get; set; }

    public AllPublicSummonerDataDTO()
    {
    }

    public AllPublicSummonerDataDTO(AllPublicSummonerDataDTO.Callback callback)
    {
      this.callback = callback;
    }

    public AllPublicSummonerDataDTO(TypedObject result)
    {
      this.SetFields<AllPublicSummonerDataDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AllPublicSummonerDataDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AllPublicSummonerDataDTO result);
  }
}
