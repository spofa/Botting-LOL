// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Harassment.LcdsResponseString
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Harassment
{
  public class LcdsResponseString : RiotGamesObject
  {
    private string type = "com.riotgames.platform.harassment.LcdsResponseString";
    private LcdsResponseString.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("value")]
    public string Value { get; set; }

    public LcdsResponseString()
    {
    }

    public LcdsResponseString(LcdsResponseString.Callback callback)
    {
      this.callback = callback;
    }

    public LcdsResponseString(TypedObject result)
    {
      this.SetFields<LcdsResponseString>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<LcdsResponseString>(this, result);
      this.callback(this);
    }

    public delegate void Callback(LcdsResponseString result);
  }
}
