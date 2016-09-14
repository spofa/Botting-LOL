// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.PlayerParticipant
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class PlayerParticipant : Participant
  {
    private string type = "com.riotgames.platform.game.PlayerParticipant";
    private PlayerParticipant.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("timeAddedToQueue")]
    public object TimeAddedToQueue { get; set; }

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
    public object TeamParticipantId { get; set; }

    public PlayerParticipant()
    {
    }

    public PlayerParticipant(PlayerParticipant.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerParticipant(TypedObject result)
    {
      this.SetFields<PlayerParticipant>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerParticipant>(this, result);
      this.callback(this);
    }

    public override string ToString()
    {
      return this.SummonerName;
    }

    public delegate void Callback(PlayerParticipant result);
  }
}
