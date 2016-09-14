﻿// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Matchmaking.MatchMakerParams
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Matchmaking
{
  public class MatchMakerParams : RiotGamesObject
  {
    private string type = "com.riotgames.platform.matchmaking.MatchMakerParams";
    private MatchMakerParams.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("lastMaestroMessage")]
    public object LastMaestroMessage { get; set; }

    [InternalName("teamId")]
    public object TeamId { get; set; }

    [InternalName("languages")]
    public object Languages { get; set; }

    [InternalName("botDifficulty")]
    public string BotDifficulty { get; set; }

    [InternalName("team")]
    public object Team { get; set; }

    [InternalName("queueIds")]
    public int[] QueueIds { get; set; }

    [InternalName("invitationId")]
    public object InvitationId { get; set; }

    public MatchMakerParams()
    {
    }

    public MatchMakerParams(MatchMakerParams.Callback callback)
    {
      this.callback = callback;
    }

    public MatchMakerParams(TypedObject result)
    {
      this.SetFields<MatchMakerParams>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<MatchMakerParams>(this, result);
      this.callback(this);
    }

    public delegate void Callback(MatchMakerParams result);
  }
}
