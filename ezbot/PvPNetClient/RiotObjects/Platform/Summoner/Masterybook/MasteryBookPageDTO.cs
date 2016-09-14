// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Masterybook.MasteryBookPageDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner.Masterybook
{
  public class MasteryBookPageDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.masterybook.MasteryBookPageDTO";
    private MasteryBookPageDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("talentEntries")]
    public List<TalentEntry> TalentEntries { get; set; }

    [InternalName("pageId")]
    public double PageId { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("current")]
    public bool Current { get; set; }

    [InternalName("createDate")]
    public object CreateDate { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public MasteryBookPageDTO()
    {
    }

    public MasteryBookPageDTO(MasteryBookPageDTO.Callback callback)
    {
      this.callback = callback;
    }

    public MasteryBookPageDTO(TypedObject result)
    {
      this.SetFields<MasteryBookPageDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<MasteryBookPageDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(MasteryBookPageDTO result);
  }
}
