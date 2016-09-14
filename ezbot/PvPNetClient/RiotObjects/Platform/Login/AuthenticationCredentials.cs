// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Login.AuthenticationCredentials
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Login
{
  public class AuthenticationCredentials : RiotGamesObject
  {
    private string type = "com.riotgames.platform.login.AuthenticationCredentials";
    private AuthenticationCredentials.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("oldPassword")]
    public object OldPassword { get; set; }

    [InternalName("username")]
    public string Username { get; set; }

    [InternalName("securityAnswer")]
    public object SecurityAnswer { get; set; }

    [InternalName("password")]
    public string Password { get; set; }

    [InternalName("partnerCredentials")]
    public object PartnerCredentials { get; set; }

    [InternalName("domain")]
    public string Domain { get; set; }

    [InternalName("ipAddress")]
    public string IpAddress { get; set; }

    [InternalName("clientVersion")]
    public string ClientVersion { get; set; }

    [InternalName("locale")]
    public string Locale { get; set; }

    [InternalName("authToken")]
    public string AuthToken { get; set; }

    [InternalName("operatingSystem")]
    public string OperatingSystem { get; set; }

    public AuthenticationCredentials()
    {
    }

    public AuthenticationCredentials(AuthenticationCredentials.Callback callback)
    {
      this.callback = callback;
    }

    public AuthenticationCredentials(TypedObject result)
    {
      this.SetFields<AuthenticationCredentials>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AuthenticationCredentials>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AuthenticationCredentials result);
  }
}
