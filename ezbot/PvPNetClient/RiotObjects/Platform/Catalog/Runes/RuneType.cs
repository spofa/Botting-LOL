// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Catalog.Runes.RuneType
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Catalog.Runes
{
  public class RuneType : RiotGamesObject
  {
    private string type = "com.riotgames.platform.catalog.runes.RuneType";
    private RuneType.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("runeTypeId")]
    public int RuneTypeId { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    public RuneType()
    {
    }

    public RuneType(RuneType.Callback callback)
    {
      this.callback = callback;
    }

    public RuneType(TypedObject result)
    {
      this.SetFields<RuneType>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<RuneType>(this, result);
      this.callback(this);
    }

    public delegate void Callback(RuneType result);
  }
}
