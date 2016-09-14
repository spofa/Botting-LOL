// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.Spellbook.SlotEntry
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Summoner.Spellbook
{
  public class SlotEntry : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.spellbook.SlotEntry";
    private SlotEntry.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("runeId")]
    public int RuneId { get; set; }

    [InternalName("runeSlotId")]
    public int RuneSlotId { get; set; }

    public SlotEntry()
    {
    }

    public SlotEntry(SlotEntry.Callback callback)
    {
      this.callback = callback;
    }

    public SlotEntry(TypedObject result)
    {
      this.SetFields<SlotEntry>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SlotEntry>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SlotEntry result);
  }
}
