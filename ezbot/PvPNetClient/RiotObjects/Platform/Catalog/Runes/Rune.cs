// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.Runes.Rune
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Catalog.Runes
{
  public class Rune : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.runes.Rune";
    private Rune.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("imagePath")]
    public object ImagePath { get; set; }

    [InternalName("toolTip")]
    public object ToolTip { get; set; }

    [InternalName("tier")]
    public int Tier { get; set; }

    [InternalName("itemId")]
    public int ItemId { get; set; }

    [InternalName("runeType")]
    public RuneType RuneType { get; set; }

    [InternalName("duration")]
    public int Duration { get; set; }

    [InternalName("gameCode")]
    public int GameCode { get; set; }

    [InternalName("itemEffects")]
    public List<ItemEffect> ItemEffects { get; set; }

    [InternalName("baseType")]
    public string BaseType { get; set; }

    [InternalName("description")]
    public string Description { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("uses")]
    public object Uses { get; set; }

    public Rune()
    {
    }

    public Rune(Rune.Callback callback)
    {
      this.callback = callback;
    }

    public Rune(TypedObject result)
    {
      this.SetFields<Rune>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<Rune>(this, result);
      this.callback(this);
    }

    public delegate void Callback(Rune result);
  }
}
