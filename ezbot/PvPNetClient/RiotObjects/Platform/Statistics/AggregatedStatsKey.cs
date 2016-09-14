// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.AggregatedStatsKey
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class AggregatedStatsKey : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.AggregatedStatsKey";
    private AggregatedStatsKey.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("gameMode")]
    public string GameMode { get; set; }

    [InternalName("userId")]
    public double UserId { get; set; }

    [InternalName("gameModeString")]
    public string GameModeString { get; set; }

    public AggregatedStatsKey()
    {
    }

    public AggregatedStatsKey(AggregatedStatsKey.Callback callback)
    {
      this.callback = callback;
    }

    public AggregatedStatsKey(TypedObject result)
    {
      this.SetFields<AggregatedStatsKey>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AggregatedStatsKey>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AggregatedStatsKey result);
  }
}
