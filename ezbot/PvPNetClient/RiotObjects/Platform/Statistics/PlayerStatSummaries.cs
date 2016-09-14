// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.PlayerStatSummaries
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class PlayerStatSummaries : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.PlayerStatSummaries";
    private PlayerStatSummaries.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("playerStatSummarySet")]
    public List<PlayerStatSummary> PlayerStatSummarySet { get; set; }

    [InternalName("userId")]
    public double UserId { get; set; }

    public PlayerStatSummaries()
    {
    }

    public PlayerStatSummaries(PlayerStatSummaries.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerStatSummaries(TypedObject result)
    {
      this.SetFields<PlayerStatSummaries>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerStatSummaries>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerStatSummaries result);
  }
}
