// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.BotParticipant
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Catalog.Champion;

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class BotParticipant : Participant
  {
    private string type = "com.riotgames.platform.game.BotParticipant";
    private BotParticipant.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("botSkillLevel")]
    public int BotSkillLevel { get; set; }

    [InternalName("champion")]
    public ChampionDTO Champion { get; set; }

    [InternalName("botSkillLevelName")]
    public string BotSkillLevelName { get; set; }

    [InternalName("teamId")]
    public string TeamId { get; set; }

    [InternalName("isGameOwner")]
    public bool IsGameOwner { get; set; }

    [InternalName("pickMode")]
    public int PickMode { get; set; }

    [InternalName("team")]
    public int Team { get; set; }

    [InternalName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [InternalName("pickTurn")]
    public int PickTurn { get; set; }

    [InternalName("badges")]
    public int Badges { get; set; }

    [InternalName("isMe")]
    public bool IsMe { get; set; }

    [InternalName("summonerName")]
    public string SummonerName { get; set; }

    [InternalName("teamName")]
    public object TeamName { get; set; }

    public BotParticipant()
    {
    }

    public BotParticipant(BotParticipant.Callback callback)
    {
      this.callback = callback;
    }

    public BotParticipant(TypedObject result)
    {
      this.SetFields<BotParticipant>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<BotParticipant>(this, result);
      this.callback(this);
    }

    public delegate void Callback(BotParticipant result);
  }
}
