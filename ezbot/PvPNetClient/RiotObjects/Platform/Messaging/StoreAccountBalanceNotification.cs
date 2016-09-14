// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Messaging.StoreAccountBalanceNotification
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Messaging
{
  internal class StoreAccountBalanceNotification : RiotGamesObject
  {
    private string type = "com.riotgames.platform.reroll.pojo.StoreAccountBalanceNotification";
    private StoreAccountBalanceNotification.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("rp")]
    public double Rp { get; set; }

    [InternalName("ip")]
    public double Ip { get; set; }

    public StoreAccountBalanceNotification()
    {
    }

    public StoreAccountBalanceNotification(StoreAccountBalanceNotification.Callback callback)
    {
      this.callback = callback;
    }

    public StoreAccountBalanceNotification(TypedObject result)
    {
      this.SetFields<StoreAccountBalanceNotification>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<StoreAccountBalanceNotification>(this, result);
      this.callback(this);
    }

    public delegate void Callback(StoreAccountBalanceNotification result);
  }
}
