// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.PlayerCredentialsDto
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class PlayerCredentialsDto : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.PlayerCredentialsDto";
    private PlayerCredentialsDto.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("encryptionKey")]
    public string EncryptionKey { get; set; }

    [InternalName("gameId")]
    public double GameId { get; set; }

    [InternalName("lastSelectedSkinIndex")]
    public int LastSelectedSkinIndex { get; set; }

    [InternalName("serverIp")]
    public string ServerIp { get; set; }

    [InternalName("observer")]
    public bool Observer { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    [InternalName("observerServerIp")]
    public string ObserverServerIp { get; set; }

    [InternalName("handshakeToken")]
    public string HandshakeToken { get; set; }

    [InternalName("playerId")]
    public double PlayerId { get; set; }

    [InternalName("serverPort")]
    public int ServerPort { get; set; }

    [InternalName("observerServerPort")]
    public int ObserverServerPort { get; set; }

    [InternalName("summonerName")]
    public string SummonerName { get; set; }

    [InternalName("observerEncryptionKey")]
    public string ObserverEncryptionKey { get; set; }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    public PlayerCredentialsDto()
    {
    }

    public PlayerCredentialsDto(PlayerCredentialsDto.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerCredentialsDto(TypedObject result)
    {
      this.SetFields<PlayerCredentialsDto>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerCredentialsDto>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerCredentialsDto result);
  }
}
