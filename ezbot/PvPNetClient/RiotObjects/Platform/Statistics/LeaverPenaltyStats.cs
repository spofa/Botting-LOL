// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.LeaverPenaltyStats
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class LeaverPenaltyStats : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.LeaverPenaltyStats";
    private LeaverPenaltyStats.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("lastLevelIncrease")]
    public object LastLevelIncrease { get; set; }

    [InternalName("level")]
    public int Level { get; set; }

    [InternalName("lastDecay")]
    public DateTime LastDecay { get; set; }

    [InternalName("userInformed")]
    public bool UserInformed { get; set; }

    [InternalName("points")]
    public int Points { get; set; }

    public LeaverPenaltyStats()
    {
    }

    public LeaverPenaltyStats(LeaverPenaltyStats.Callback callback)
    {
      this.callback = callback;
    }

    public LeaverPenaltyStats(TypedObject result)
    {
      this.SetFields<LeaverPenaltyStats>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<LeaverPenaltyStats>(this, result);
      this.callback(this);
    }

    public delegate void Callback(LeaverPenaltyStats result);
  }
}
