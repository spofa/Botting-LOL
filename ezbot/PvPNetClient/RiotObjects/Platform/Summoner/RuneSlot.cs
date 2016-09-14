// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Summoner.RuneSlot
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Catalog.Runes;

namespace PvPNetClient.RiotObjects.Platform.Summoner
{
  public class RuneSlot : RiotGamesObject
  {
    private string type = "com.riotgames.platform.summoner.RuneSlot";
    private RuneSlot.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("id")]
    public int Id { get; set; }

    [InternalName("minLevel")]
    public int MinLevel { get; set; }

    [InternalName("runeType")]
    public RuneType RuneType { get; set; }

    public RuneSlot()
    {
    }

    public RuneSlot(RuneSlot.Callback callback)
    {
      this.callback = callback;
    }

    public RuneSlot(TypedObject result)
    {
      this.SetFields<RuneSlot>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<RuneSlot>(this, result);
      this.callback(this);
    }

    public delegate void Callback(RuneSlot result);
  }
}
