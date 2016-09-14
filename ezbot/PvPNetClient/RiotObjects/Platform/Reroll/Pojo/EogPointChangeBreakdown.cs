// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Reroll.Pojo.EogPointChangeBreakdown
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Reroll.Pojo
{
  internal class EogPointChangeBreakdown : RiotGamesObject
  {
    private string type = "com.riotgames.platform.reroll.pojo.EogPointChangeBreakdown";
    private EogPointChangeBreakdown.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("pointChangeFromGamePlay")]
    public double PointChangeFromGamePlay { get; set; }

    [InternalName("pointChangeFromChampionsOwned")]
    public double PointChangeFromChampionsOwned { get; set; }

    [InternalName("previousPoints")]
    public double PreviousPoints { get; set; }

    [InternalName("pointsUsed")]
    public double PointsUsed { get; set; }

    [InternalName("endPoints")]
    public double EndPoints { get; set; }

    public EogPointChangeBreakdown()
    {
    }

    public EogPointChangeBreakdown(EogPointChangeBreakdown.Callback callback)
    {
      this.callback = callback;
    }

    public EogPointChangeBreakdown(TypedObject result)
    {
      this.SetFields<EogPointChangeBreakdown>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<EogPointChangeBreakdown>(this, result);
      this.callback(this);
    }

    public delegate void Callback(EogPointChangeBreakdown result);
  }
}
