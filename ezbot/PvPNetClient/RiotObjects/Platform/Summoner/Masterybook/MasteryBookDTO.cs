// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Masterybook.MasteryBookDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner.Masterybook
{
  public class MasteryBookDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.masterybook.MasteryBookDTO";
    private MasteryBookDTO.Callback callback;

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
    public List<MasteryBookPageDTO> BookPages { get; set; }

    [InternalName("dateString")]
    public string DateString { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public MasteryBookDTO()
    {
    }

    public MasteryBookDTO(MasteryBookDTO.Callback callback)
    {
      this.callback = callback;
    }

    public MasteryBookDTO(TypedObject result)
    {
      this.SetFields<MasteryBookDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<MasteryBookDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(MasteryBookDTO result);
  }
}
