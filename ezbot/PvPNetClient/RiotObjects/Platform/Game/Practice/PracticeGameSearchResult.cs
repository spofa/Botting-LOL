// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.Practice.PracticeGameSearchResult
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game.Practice
{
  public class PracticeGameSearchResult : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.practice.PracticeGameSearchResult";
    private PracticeGameSearchResult.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("spectatorCount")]
    public int SpectatorCount { get; set; }

    [InternalName("glmGameId")]
    public object GlmGameId { get; set; }

    [InternalName("glmHost")]
    public object GlmHost { get; set; }

    [InternalName("glmPort")]
    public int GlmPort { get; set; }

    [InternalName("gameModeString")]
    public string GameModeString { get; set; }

    [InternalName("allowSpectators")]
    public string AllowSpectators { get; set; }

    [InternalName("gameMapId")]
    public int GameMapId { get; set; }

    [InternalName("maxNumPlayers")]
    public int MaxNumPlayers { get; set; }

    [InternalName("glmSecurePort")]
    public int GlmSecurePort { get; set; }

    [InternalName("gameMode")]
    public string GameMode { get; set; }

    [InternalName("id")]
    public double Id { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("privateGame")]
    public bool PrivateGame { get; set; }

    [InternalName("owner")]
    public PlayerParticipant Owner { get; set; }

    [InternalName("team1Count")]
    public int Team1Count { get; set; }

    [InternalName("team2Count")]
    public int Team2Count { get; set; }

    public PracticeGameSearchResult()
    {
    }

    public PracticeGameSearchResult(PracticeGameSearchResult.Callback callback)
    {
      this.callback = callback;
    }

    public PracticeGameSearchResult(TypedObject result)
    {
      this.SetFields<PracticeGameSearchResult>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PracticeGameSearchResult>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PracticeGameSearchResult result);
  }
}
