// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Summoner
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class Summoner : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.Summoner";
    private PvPNetClient.RiotObjects.Platform.Summoner.Summoner.Callback callback;

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

    [PvPNetClient.RiotObjects.InternalName("acctId")]
    public double AcctId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("helpFlag")]
    public bool HelpFlag { get; set; }

    [PvPNetClient.RiotObjects.InternalName("sumId")]
    public double SumId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("profileIconId")]
    public int ProfileIconId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("displayEloQuestionaire")]
    public bool DisplayEloQuestionaire { get; set; }

    [PvPNetClient.RiotObjects.InternalName("lastGameDate")]
    public DateTime LastGameDate { get; set; }

    [PvPNetClient.RiotObjects.InternalName("advancedTutorialFlag")]
    public bool AdvancedTutorialFlag { get; set; }

    [PvPNetClient.RiotObjects.InternalName("revisionDate")]
    public DateTime RevisionDate { get; set; }

    [PvPNetClient.RiotObjects.InternalName("revisionId")]
    public double RevisionId { get; set; }

    [PvPNetClient.RiotObjects.InternalName("seasonOneTier")]
    public string SeasonOneTier { get; set; }

    [PvPNetClient.RiotObjects.InternalName("name")]
    public string Name { get; set; }

    [PvPNetClient.RiotObjects.InternalName("nameChangeFlag")]
    public bool NameChangeFlag { get; set; }

    [PvPNetClient.RiotObjects.InternalName("tutorialFlag")]
    public bool TutorialFlag { get; set; }

    [PvPNetClient.RiotObjects.InternalName("socialNetworkUserIds")]
    public List<object> SocialNetworkUserIds { get; set; }

    public Summoner()
    {
    }

    public Summoner(PvPNetClient.RiotObjects.Platform.Summoner.Summoner.Callback callback)
    {
      this.callback = callback;
    }

    public Summoner(TypedObject result)
    {
      this.SetFields<PvPNetClient.RiotObjects.Platform.Summoner.Summoner>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PvPNetClient.RiotObjects.Platform.Summoner.Summoner>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PvPNetClient.RiotObjects.Platform.Summoner.Summoner result);
  }
}
