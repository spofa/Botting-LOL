// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.SummonerLevelAndPoints
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class SummonerLevelAndPoints : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.SummonerLevelAndPoints";
    private SummonerLevelAndPoints.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("infPoints")]
    public double InfPoints { get; set; }

    [InternalName("expPoints")]
    public double ExpPoints { get; set; }

    [InternalName("summonerLevel")]
    public double SummonerLevel { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public SummonerLevelAndPoints()
    {
    }

    public SummonerLevelAndPoints(SummonerLevelAndPoints.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerLevelAndPoints(TypedObject result)
    {
      this.SetFields<SummonerLevelAndPoints>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerLevelAndPoints>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerLevelAndPoints result);
  }
}
