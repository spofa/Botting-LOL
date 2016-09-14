// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Boost.SummonerActiveBoostsDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner.Boost
{
  public class SummonerActiveBoostsDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.boost.SummonerActiveBoostsDTO";
    private SummonerActiveBoostsDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("xpBoostEndDate")]
    public double XpBoostEndDate { get; set; }

    [InternalName("xpBoostPerWinCount")]
    public int XpBoostPerWinCount { get; set; }

    [InternalName("xpLoyaltyBoost")]
    public int XpLoyaltyBoost { get; set; }

    [InternalName("ipBoostPerWinCount")]
    public int IpBoostPerWinCount { get; set; }

    [InternalName("ipLoyaltyBoost")]
    public int IpLoyaltyBoost { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    [InternalName("ipBoostEndDate")]
    public double IpBoostEndDate { get; set; }

    public SummonerActiveBoostsDTO()
    {
    }

    public SummonerActiveBoostsDTO(SummonerActiveBoostsDTO.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerActiveBoostsDTO(TypedObject result)
    {
      this.SetFields<SummonerActiveBoostsDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerActiveBoostsDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerActiveBoostsDTO result);
  }
}
