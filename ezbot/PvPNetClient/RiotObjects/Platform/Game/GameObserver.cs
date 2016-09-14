// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.GameObserver
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class GameObserver : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.GameObserver";
    private GameObserver.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("accountId")]
    public double AccountId { get; set; }

    [InternalName("botDifficulty")]
    public string BotDifficulty { get; set; }

    [InternalName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [InternalName("locale")]
    public object Locale { get; set; }

    [InternalName("lastSelectedSkinIndex")]
    public int LastSelectedSkinIndex { get; set; }

    [InternalName("partnerId")]
    public string PartnerId { get; set; }

    [InternalName("profileIconId")]
    public int ProfileIconId { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    [InternalName("badges")]
    public int Badges { get; set; }

    [InternalName("pickTurn")]
    public int PickTurn { get; set; }

    [InternalName("originalAccountId")]
    public double OriginalAccountId { get; set; }

    [InternalName("summonerName")]
    public string SummonerName { get; set; }

    [InternalName("pickMode")]
    public int PickMode { get; set; }

    [InternalName("originalPlatformId")]
    public string OriginalPlatformId { get; set; }

    public GameObserver()
    {
    }

    public GameObserver(GameObserver.Callback callback)
    {
      this.callback = callback;
    }

    public GameObserver(TypedObject result)
    {
      this.SetFields<GameObserver>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<GameObserver>(this, result);
      this.callback(this);
    }

    public delegate void Callback(GameObserver result);
  }
}
