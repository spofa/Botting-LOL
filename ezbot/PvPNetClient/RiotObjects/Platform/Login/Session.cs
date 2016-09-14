// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Login.Session
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using PvPNetClient.RiotObjects.Platform.Account;

namespace PvPNetClient.RiotObjects.Platform.Login
{
  public class Session : RiotGamesObject
  {
    private string type = "com.riotgames.platform.login.Session";
    private Session.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("token")]
    public string Token { get; set; }

    [InternalName("password")]
    public string Password { get; set; }

    [InternalName("accountSummary")]
    public AccountSummary AccountSummary { get; set; }

    public Session()
    {
    }

    public Session(Session.Callback callback)
    {
      this.callback = callback;
    }

    public Session(TypedObject result)
    {
      this.SetFields<Session>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<Session>(this, result);
      this.callback(this);
    }

    public delegate void Callback(Session result);
  }
}
