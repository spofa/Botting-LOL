// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Broadcast.BroadcastNotification
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Broadcast
{
  public class BroadcastNotification : RiotGamesObject
  {
    private string type = "com.riotgames.platform.broadcast.BroadcastNotification";
    private BroadcastNotification.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    public BroadcastNotification()
    {
    }

    public BroadcastNotification(BroadcastNotification.Callback callback)
    {
      this.callback = callback;
    }

    public BroadcastNotification(TypedObject result)
    {
      this.SetFields<BroadcastNotification>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<BroadcastNotification>(this, result);
      this.callback(this);
    }

    public delegate void Callback(BroadcastNotification result);
  }
}
