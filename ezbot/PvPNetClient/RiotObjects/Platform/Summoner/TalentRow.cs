// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.TalentRow
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class TalentRow : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.TalentRow";
    private TalentRow.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("index")]
    public int Index { get; set; }

    [InternalName("talents")]
    public List<Talent> Talents { get; set; }

    [InternalName("tltGroupId")]
    public int TltGroupId { get; set; }

    [InternalName("pointsToActivate")]
    public int PointsToActivate { get; set; }

    [InternalName("tltRowId")]
    public int TltRowId { get; set; }

    public TalentRow()
    {
    }

    public TalentRow(TalentRow.Callback callback)
    {
      this.callback = callback;
    }

    public TalentRow(TypedObject result)
    {
      this.SetFields<TalentRow>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TalentRow>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TalentRow result);
  }
}
