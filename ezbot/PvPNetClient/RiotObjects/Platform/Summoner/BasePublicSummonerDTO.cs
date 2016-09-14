// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.BasePublicSummonerDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class BasePublicSummonerDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.BasePublicSummonerDTO";
    private BasePublicSummonerDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [PvPNetClient.RiotObjects.InternalName("seasonTwoTier")]
    public string SeasonTwoTier { get; set; }

    [PvPNetClient.RiotObjects.InternalName("internalName")]
    public string InternalName { get; set; }

    [PvPNetClient.RiotObjects.InternalName("seasonOneTier")]
    public string SeasonOneTier { get; set; }

    [PvPNetClient.RiotObjects.InternalName("acctId")]
    public double AcctId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("name")]
    public string Name { get; set; }

    [PvPNetClient.RiotObjects.InternalName("sumId")]
    public double SumId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("profileIconId")]
    public int ProfileIconId { get; set; }

    public BasePublicSummonerDTO()
    {
    }

    public BasePublicSummonerDTO(BasePublicSummonerDTO.Callback callback)
    {
      this.callback = callback;
    }

    public BasePublicSummonerDTO(TypedObject result)
    {
      this.SetFields<BasePublicSummonerDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<BasePublicSummonerDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(BasePublicSummonerDTO result);
  }
}
