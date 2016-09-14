// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.FellowPlayerInfo
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class FellowPlayerInfo : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.FellowPlayerInfo";
    private FellowPlayerInfo.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("championId")]
    public double ChampionId { get; set; }

    [InternalName("teamId")]
    public int TeamId { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public FellowPlayerInfo()
    {
    }

    public FellowPlayerInfo(FellowPlayerInfo.Callback callback)
    {
      this.callback = callback;
    }

    public FellowPlayerInfo(TypedObject result)
    {
      this.SetFields<FellowPlayerInfo>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<FellowPlayerInfo>(this, result);
      this.callback(this);
    }

    public delegate void Callback(FellowPlayerInfo result);
  }
}
