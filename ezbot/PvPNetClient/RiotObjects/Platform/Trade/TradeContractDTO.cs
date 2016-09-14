// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Trade.TradeContractDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Trade
{
  public class TradeContractDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.trade.api.contract.TradeContractDTO";
    private TradeContractDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("requesterInternalSummonerName")]
    public string RequesterInternalSummonerName { get; set; }

    [InternalName("requesterChampionId")]
    public double RequesterChampionId { get; set; }

    [InternalName("state")]
    public string State { get; set; }

    [InternalName("responderChampionId")]
    public double ResponderChampionId { get; set; }

    [InternalName("responderInternalSummonerName")]
    public string ResponderInternalSummonerName { get; set; }

    public TradeContractDTO()
    {
    }

    public TradeContractDTO(TradeContractDTO.Callback callback)
    {
      this.callback = callback;
    }

    public TradeContractDTO(TypedObject result)
    {
      this.SetFields<TradeContractDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TradeContractDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TradeContractDTO result);
  }
}
