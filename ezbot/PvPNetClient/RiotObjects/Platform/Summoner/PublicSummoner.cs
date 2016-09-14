// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.PublicSummoner
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class PublicSummoner : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.PublicSummoner";
    private PublicSummoner.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [PvPNetClient.RiotObjects.InternalName("internalName")]
    public string InternalName { get; set; }

    [PvPNetClient.RiotObjects.InternalName("acctId")]
    public double AcctId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("name")]
    public string Name { get; set; }

    [PvPNetClient.RiotObjects.InternalName("profileIconId")]
    public int ProfileIconId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("revisionDate")]
    public DateTime RevisionDate { get; set; }

    [PvPNetClient.RiotObjects.InternalName("revisionId")]
    public double RevisionId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("summonerLevel")]
    public double SummonerLevel { get; set; }

    [PvPNetClient.RiotObjects.InternalName("summonerId")]
    public double SummonerId { get; set; }

    public PublicSummoner()
    {
    }

    public PublicSummoner(PublicSummoner.Callback callback)
    {
      this.callback = callback;
    }

    public PublicSummoner(TypedObject result)
    {
      this.SetFields<PublicSummoner>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PublicSummoner>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PublicSummoner result);
  }
}
