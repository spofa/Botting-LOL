// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.TalentGroup
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class TalentGroup : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.TalentGroup";
    private TalentGroup.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("index")]
    public int Index { get; set; }

    [InternalName("talentRows")]
    public List<TalentRow> TalentRows { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("tltGroupId")]
    public int TltGroupId { get; set; }

    public TalentGroup()
    {
    }

    public TalentGroup(TalentGroup.Callback callback)
    {
      this.callback = callback;
    }

    public TalentGroup(TypedObject result)
    {
      this.SetFields<TalentGroup>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TalentGroup>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TalentGroup result);
  }
}
