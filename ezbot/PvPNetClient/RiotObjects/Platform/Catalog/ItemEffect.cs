// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.ItemEffect
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Catalog
{
  public class ItemEffect : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.ItemEffect";
    private ItemEffect.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("effectId")]
    public int EffectId { get; set; }

    [InternalName("itemEffectId")]
    public int ItemEffectId { get; set; }

    [InternalName("effect")]
    public Effect Effect { get; set; }

    [InternalName("value")]
    public string Value { get; set; }

    [InternalName("itemId")]
    public int ItemId { get; set; }

    public ItemEffect()
    {
    }

    public ItemEffect(ItemEffect.Callback callback)
    {
      this.callback = callback;
    }

    public ItemEffect(TypedObject result)
    {
      this.SetFields<ItemEffect>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ItemEffect>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ItemEffect result);
  }
}
