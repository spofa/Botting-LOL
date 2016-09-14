// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.AggregatedStat
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class AggregatedStat : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.AggregatedStat";
    private AggregatedStat.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("statType")]
    public string StatType { get; set; }

    [InternalName("count")]
    public double Count { get; set; }

    [InternalName("value")]
    public double Value { get; set; }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    public AggregatedStat()
    {
    }

    public AggregatedStat(AggregatedStat.Callback callback)
    {
      this.callback = callback;
    }

    public AggregatedStat(TypedObject result)
    {
      this.SetFields<AggregatedStat>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AggregatedStat>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AggregatedStat result);
  }
}
