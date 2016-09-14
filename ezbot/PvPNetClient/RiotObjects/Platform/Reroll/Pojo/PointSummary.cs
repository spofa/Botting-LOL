// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Reroll.Pojo.PointSummary
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Reroll.Pojo
{
  public class PointSummary : RiotGamesObject
  {
    private string type = "com.riotgames.platform.reroll.pojo.PointSummary";
    private PointSummary.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("pointsToNextRoll")]
    public double PointsToNextRoll { get; set; }

    [InternalName("maxRolls")]
    public int MaxRolls { get; set; }

    [InternalName("numberOfRolls")]
    public int NumberOfRolls { get; set; }

    [InternalName("pointsCostToRoll")]
    public double PointsCostToRoll { get; set; }

    [InternalName("currentPoints")]
    public double CurrentPoints { get; set; }

    public PointSummary()
    {
    }

    public PointSummary(PointSummary.Callback callback)
    {
      this.callback = callback;
    }

    public PointSummary(TypedObject result)
    {
      this.SetFields<PointSummary>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PointSummary>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PointSummary result);
  }
}
