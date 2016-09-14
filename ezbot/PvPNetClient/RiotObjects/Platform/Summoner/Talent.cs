﻿// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Talent
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class Talent : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.Talent";
    private Talent.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("index")]
    public int Index { get; set; }

    [InternalName("level5Desc")]
    public string Level5Desc { get; set; }

    [InternalName("minLevel")]
    public int MinLevel { get; set; }

    [InternalName("maxRank")]
    public int MaxRank { get; set; }

    [InternalName("level4Desc")]
    public string Level4Desc { get; set; }

    [InternalName("tltId")]
    public int TltId { get; set; }

    [InternalName("level3Desc")]
    public string Level3Desc { get; set; }

    [InternalName("talentGroupId")]
    public int TalentGroupId { get; set; }

    [InternalName("gameCode")]
    public int GameCode { get; set; }

    [InternalName("minTier")]
    public int MinTier { get; set; }

    [InternalName("prereqTalentGameCode")]
    public object PrereqTalentGameCode { get; set; }

    [InternalName("level2Desc")]
    public string Level2Desc { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("talentRowId")]
    public int TalentRowId { get; set; }

    [InternalName("level1Desc")]
    public string Level1Desc { get; set; }

    public Talent()
    {
    }

    public Talent(Talent.Callback callback)
    {
      this.callback = callback;
    }

    public Talent(TypedObject result)
    {
      this.SetFields<Talent>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<Talent>(this, result);
      this.callback(this);
    }

    public delegate void Callback(Talent result);
  }
}
