// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.SummonerTalentsAndPoints
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class SummonerTalentsAndPoints : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.SummonerTalentsAndPoints";
    private SummonerTalentsAndPoints.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("talentPoints")]
    public int TalentPoints { get; set; }

    [InternalName("modifyDate")]
    public DateTime ModifyDate { get; set; }

    [InternalName("createDate")]
    public DateTime CreateDate { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public SummonerTalentsAndPoints()
    {
    }

    public SummonerTalentsAndPoints(SummonerTalentsAndPoints.Callback callback)
    {
      this.callback = callback;
    }

    public SummonerTalentsAndPoints(TypedObject result)
    {
      this.SetFields<SummonerTalentsAndPoints>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SummonerTalentsAndPoints>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SummonerTalentsAndPoints result);
  }
}
