// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Messaging.StoreFulfillmentNotification
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Catalog.Champion;

namespace PvPNetClient.RiotObjects.Platform.Messaging
{
  internal class StoreFulfillmentNotification : RiotGamesObject
  {
    private string type = "com.riotgames.platform.reroll.pojo.StoreFulfillmentNotification";
    private StoreFulfillmentNotification.Callback callback;

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

    [InternalName("inventoryType")]
    public string InventoryType { get; set; }

    [InternalName("data")]
    public ChampionDTO Data { get; set; }

    public StoreFulfillmentNotification()
    {
    }

    public StoreFulfillmentNotification(StoreFulfillmentNotification.Callback callback)
    {
      this.callback = callback;
    }

    public StoreFulfillmentNotification(TypedObject result)
    {
      this.SetFields<StoreFulfillmentNotification>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<StoreFulfillmentNotification>(this, result);
      this.callback(this);
    }

    public delegate void Callback(StoreFulfillmentNotification result);
  }
}
