// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.Message.GameNotification
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game.Message
{
  public class GameNotification : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.message.GameNotification";
    private GameNotification.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("messageCode")]
    public string MessageCode { get; set; }

    [InternalName("type")]
    public string Type { get; set; }

    [InternalName("messageArgument")]
    public object MessageArgument { get; set; }

    public GameNotification()
    {
    }

    public GameNotification(GameNotification.Callback callback)
    {
      this.callback = callback;
    }

    public GameNotification(TypedObject result)
    {
      this.SetFields<GameNotification>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<GameNotification>(this, result);
      this.callback(this);
    }

    public delegate void Callback(GameNotification result);
  }
}
