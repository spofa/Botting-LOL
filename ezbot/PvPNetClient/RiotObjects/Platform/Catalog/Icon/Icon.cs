// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects.Platform.Catalog.Icon
{
  public class Icon : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.icon.Icon";
    private PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("purchaseDate")]
    public DateTime PurchaseDate { get; set; }

    [InternalName("iconId")]
    public double IconId { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public Icon()
    {
    }

    public Icon(PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon.Callback callback)
    {
      this.callback = callback;
    }

    public Icon(TypedObject result)
    {
      this.SetFields<PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PvPNetClient.RiotObjects.Platform.Catalog.Icon.Icon result);
  }
}
