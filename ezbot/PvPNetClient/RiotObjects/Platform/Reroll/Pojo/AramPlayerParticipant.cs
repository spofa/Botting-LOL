﻿// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Reroll.Pojo.AramPlayerParticipant
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Game;

namespace PvPNetClient.RiotObjects.Platform.Reroll.Pojo
{
  public class AramPlayerParticipant : Participant
  {
    private string type = "com.riotgames.platform.reroll.pojo.AramPlayerParticipant";
    private AramPlayerParticipant.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("timeAddedToQueue")]
    public double TimeAddedToQueue { get; set; }

    [InternalName("index")]
    public int Index { get; set; }

    [InternalName("queueRating")]
    public int QueueRating { get; set; }

    [InternalName("accountId")]
    public double AccountId { get; set; }

    [InternalName("botDifficulty")]
    public string BotDifficulty { get; set; }

    [InternalName("originalAccountNumber")]
    public double OriginalAccountNumber { get; set; }

    [InternalName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [InternalName("minor")]
    public bool Minor { get; set; }

    [InternalName("locale")]
    public object Locale { get; set; }

    [InternalName("lastSelectedSkinIndex")]
    public int LastSelectedSkinIndex { get; set; }

    [InternalName("partnerId")]
    public string PartnerId { get; set; }

    [InternalName("profileIconId")]
    public int ProfileIconId { get; set; }

    [InternalName("teamOwner")]
    public bool TeamOwner { get; set; }

    [InternalName("pointSummary")]
    public PointSummary PointSummary { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    [InternalName("badges")]
    public int Badges { get; set; }

    [InternalName("pickTurn")]
    public int PickTurn { get; set; }

    [InternalName("clientInSynch")]
    public bool ClientInSynch { get; set; }

    [InternalName("summonerName")]
    public string SummonerName { get; set; }

    [InternalName("pickMode")]
    public int PickMode { get; set; }

    [InternalName("originalPlatformId")]
    public string OriginalPlatformId { get; set; }

    [InternalName("teamParticipantId")]
    public double TeamParticipantId { get; set; }

    public AramPlayerParticipant()
    {
    }

    public AramPlayerParticipant(AramPlayerParticipant.Callback callback)
    {
      this.callback = callback;
    }

    public AramPlayerParticipant(TypedObject result)
    {
      this.SetFields<AramPlayerParticipant>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AramPlayerParticipant>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AramPlayerParticipant result);
  }
}
