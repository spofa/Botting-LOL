// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.SummaryAggStat
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class SummaryAggStat : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.SummaryAggStat";
    private SummaryAggStat.Callback callback;

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

    public SummaryAggStat(SummaryAggStat.Callback callback)
    {
      this.callback = callback;
    }

    public SummaryAggStat(TypedObject result)
    {
      this.SetFields<SummaryAggStat>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummaryAggStat>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummaryAggStat result);
  }
}
