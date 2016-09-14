// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Account.AccountSummary
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Account
{
  public class AccountSummary : RiotGamesObject
  {
    private string type = "com.riotgames.platform.account.AccountSummary";
    private AccountSummary.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("groupCount")]
    public int GroupCount { get; set; }

    [InternalName("username")]
    public string Username { get; set; }

    [InternalName("accountId")]
    public double AccountId { get; set; }

    [InternalName("summonerInternalName")]
    public object SummonerInternalName { get; set; }

    [InternalName("admin")]
    public bool Admin { get; set; }

    [InternalName("hasBetaAccess")]
    public bool HasBetaAccess { get; set; }

    [InternalName("summonerName")]
    public object SummonerName { get; set; }

    [InternalName("partnerMode")]
    public bool PartnerMode { get; set; }

    [InternalName("needsPasswordReset")]
    public bool NeedsPasswordReset { get; set; }

    public AccountSummary()
    {
    }

    public AccountSummary(AccountSummary.Callback callback)
    {
      this.callback = callback;
    }

    public AccountSummary(TypedObject result)
    {
      this.SetFields<AccountSummary>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<AccountSummary>(this, result);
      this.callback(this);
    }

    public delegate void Callback(AccountSummary result);
  }
}
