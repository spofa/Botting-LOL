// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Messaging.SimpleDialogMessageResponse
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Messaging
{
  public class SimpleDialogMessageResponse : RiotGamesObject
  {
    private string type = "com.riotgames.platform.messaging.persistence.SimpleDialogMessageResponse";
    private SimpleDialogMessageResponse.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("msgId")]
    public double MsgID { get; set; }

    [InternalName("accountId")]
    public double AccountID { get; set; }

    [InternalName("command")]
    public string Command { get; set; }

    public SimpleDialogMessageResponse()
    {
    }

    public SimpleDialogMessageResponse(SimpleDialogMessageResponse.Callback callback)
    {
      this.callback = callback;
    }

    public SimpleDialogMessageResponse(TypedObject result)
    {
      this.SetFields<SimpleDialogMessageResponse>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<SimpleDialogMessageResponse>(this, result);
      this.callback(this);
    }

    public delegate void Callback(SimpleDialogMessageResponse result);
  }
}
