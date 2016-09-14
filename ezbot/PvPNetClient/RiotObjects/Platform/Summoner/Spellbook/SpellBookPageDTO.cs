// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Spellbook.SpellBookPageDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner.Spellbook
{
  public class SpellBookPageDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.spellbook.SpellBookPageDTO";
    private SpellBookPageDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("slotEntries")]
    public List<SlotEntry> SlotEntries { get; set; }

    [InternalName("summonerId")]
    public int SummonerId { get; set; }

    [InternalName("createDate")]
    public DateTime CreateDate { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("pageId")]
    public int PageId { get; set; }

    [InternalName("current")]
    public bool Current { get; set; }

    public SpellBookPageDTO()
    {
    }

    public SpellBookPageDTO(SpellBookPageDTO.Callback callback)
    {
      this.callback = callback;
    }

    public SpellBookPageDTO(TypedObject result)
    {
      this.SetFields<SpellBookPageDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SpellBookPageDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SpellBookPageDTO result);
  }
}
