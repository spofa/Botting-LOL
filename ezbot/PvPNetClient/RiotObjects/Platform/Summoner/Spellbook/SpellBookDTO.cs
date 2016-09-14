// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Spellbook.SpellBookDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner.Spellbook
{
  public class SpellBookDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.spellbook.SpellBookDTO";
    private SpellBookDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("bookPagesJson")]
    public object BookPagesJson { get; set; }

    [InternalName("bookPages")]
    public List<SpellBookPageDTO> BookPages { get; set; }

    [InternalName("dateString")]
    public string DateString { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public SpellBookDTO()
    {
    }

    public SpellBookDTO(SpellBookDTO.Callback callback)
    {
      this.callback = callback;
    }

    public SpellBookDTO(TypedObject result)
    {
      this.SetFields<SpellBookDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SpellBookDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SpellBookDTO result);
  }
}
