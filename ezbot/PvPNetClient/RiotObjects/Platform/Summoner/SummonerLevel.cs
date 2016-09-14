// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.SummonerLevel
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class SummonerLevel : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.SummonerLevel";
    private SummonerLevel.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("expTierMod")]
    public double ExpTierMod { get; set; }

    [InternalName("grantRp")]
    public double GrantRp { get; set; }

    [InternalName("expForLoss")]
    public double ExpForLoss { get; set; }

    [InternalName("summonerTier")]
    public double SummonerTier { get; set; }

    [InternalName("infTierMod")]
    public double InfTierMod { get; set; }

    [InternalName("expToNextLevel")]
    public double ExpToNextLevel { get; set; }

    [InternalName("expForWin")]
    public double ExpForWin { get; set; }

    [InternalName("summonerLevel")]
    public double Level { get; set; }

    public SummonerLevel()
    {
    }

    public SummonerLevel(SummonerLevel.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerLevel(TypedObject result)
    {
      this.SetFields<SummonerLevel>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerLevel>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerLevel result);
  }
}
