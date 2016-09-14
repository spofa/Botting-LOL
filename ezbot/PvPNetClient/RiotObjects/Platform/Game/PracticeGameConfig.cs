// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.PracticeGameConfig
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Game.Map;

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class PracticeGameConfig : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.PracticeGameConfig";
    private PracticeGameConfig.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("passbackUrl")]
    public object PassbackUrl { get; set; }

    [InternalName("gameName")]
    public string GameName { get; set; }

    [InternalName("gameTypeConfig")]
    public int GameTypeConfig { get; set; }

    [InternalName("passbackDataPacket")]
    public object PassbackDataPacket { get; set; }

    [InternalName("gamePassword")]
    public string GamePassword { get; set; }

    [InternalName("gameMap")]
    public GameMap GameMap { get; set; }

    [InternalName("gameMode")]
    public string GameMode { get; set; }

    [InternalName("allowSpectators")]
    public string AllowSpectators { get; set; }

    [InternalName("maxNumPlayers")]
    public int MaxNumPlayers { get; set; }

    [InternalName("region")]
    public string Region { get; set; }

    public PracticeGameConfig()
    {
    }

    public PracticeGameConfig(PracticeGameConfig.Callback callback)
    {
      this.callback = callback;
    }

    public PracticeGameConfig(TypedObject result)
    {
      this.SetFields<PracticeGameConfig>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PracticeGameConfig>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PracticeGameConfig result);
  }
}
