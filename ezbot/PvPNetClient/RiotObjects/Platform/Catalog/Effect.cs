// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.Effect
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Catalog.Runes;

namespace PvPNetClient.RiotObjects.Platform.Catalog
{
  public class Effect : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.Effect";
    private Effect.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("effectId")]
    public int EffectId { get; set; }

    [InternalName("gameCode")]
    public string GameCode { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("categoryId")]
    public object CategoryId { get; set; }

    [InternalName("runeType")]
    public RuneType RuneType { get; set; }

    public Effect()
    {
    }

    public Effect(Effect.Callback callback)
    {
      this.callback = callback;
    }

    public Effect(TypedObject result)
    {
      this.SetFields<Effect>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<Effect>(this, result);
      this.callback(this);
    }

    public delegate void Callback(Effect result);
  }
}
