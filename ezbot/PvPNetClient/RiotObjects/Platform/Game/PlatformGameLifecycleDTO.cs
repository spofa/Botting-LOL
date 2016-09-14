// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.PlatformGameLifecycleDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class PlatformGameLifecycleDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.PlatformGameLifecycleDTO";
    private PlatformGameLifecycleDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("gameSpecificLoyaltyRewards")]
    public object GameSpecificLoyaltyRewards { get; set; }

    [InternalName("reconnectDelay")]
    public int ReconnectDelay { get; set; }

    [InternalName("lastModifiedDate")]
    public object LastModifiedDate { get; set; }

    [InternalName("game")]
    public GameDTO Game { get; set; }

    [InternalName("playerCredentials")]
    public PlayerCredentialsDto PlayerCredentials { get; set; }

    [InternalName("gameName")]
    public string GameName { get; set; }

    [InternalName("connectivityStateEnum")]
    public object ConnectivityStateEnum { get; set; }

    public PlatformGameLifecycleDTO()
    {
    }

    public PlatformGameLifecycleDTO(PlatformGameLifecycleDTO.Callback callback)
    {
      this.callback = callback;
    }

    public PlatformGameLifecycleDTO(TypedObject result)
    {
      this.SetFields<PlatformGameLifecycleDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlatformGameLifecycleDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlatformGameLifecycleDTO result);
  }
}
