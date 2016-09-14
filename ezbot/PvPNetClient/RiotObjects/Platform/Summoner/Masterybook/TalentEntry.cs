// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Masterybook.TalentEntry
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner.Masterybook
{
  public class TalentEntry : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.masterybook.TalentEntry";
    private TalentEntry.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("rank")]
    public int Rank { get; set; }

    [InternalName("talentId")]
    public int TalentId { get; set; }

    [InternalName("talent")]
    public Talent Talent { get; set; }

    [InternalName("summonerId")]
    public double SummonerId { get; set; }

    public TalentEntry()
    {
    }

    public TalentEntry(TalentEntry.Callback callback)
    {
      this.callback = callback;
    }

    public TalentEntry(TypedObject result)
    {
      this.SetFields<TalentEntry>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<TalentEntry>(this, result);
      this.callback(this);
    }

    public delegate void Callback(TalentEntry result);
  }
}
