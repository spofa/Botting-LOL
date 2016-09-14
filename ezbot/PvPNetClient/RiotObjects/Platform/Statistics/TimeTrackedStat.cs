// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.TimeTrackedStat
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class TimeTrackedStat : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.TimeTrackedStat";
    private TimeTrackedStat.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("timestamp")]
    public DateTime Timestamp { get; set; }

    [InternalName("type")]
    public string Type { get; set; }

    public TimeTrackedStat()
    {
    }

    public TimeTrackedStat(TimeTrackedStat.Callback callback)
    {
      this.callback = callback;
    }

    public TimeTrackedStat(TypedObject result)
    {
      this.SetFields<TimeTrackedStat>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TimeTrackedStat>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TimeTrackedStat result);
  }
}
