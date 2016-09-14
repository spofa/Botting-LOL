// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Matchmaking.QueueDodger
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Matchmaking
{
  public class QueueDodger : RiotGamesObject
  {
    private string type = "com.riotgames.platform.matchmaking.QueueDodger";
    private QueueDodger.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("reasonFailed")]
    public string ReasonFailed { get; set; }

    [InternalName("accessToken")]
    public string AccessToken { get; set; }

    [InternalName("summoner")]
    public PvPNetClient.RiotObjects.Platform.Summoner.Summoner Summoner { get; set; }

    [InternalName("dodgePenaltyRemainingTime")]
    public int DodgePenaltyRemainingTime { get; set; }

    [InternalName("leaverPenaltyMillisRemaining")]
    public int LeaverPenaltyMillisRemaining { get; set; }

    public QueueDodger()
    {
    }

    public QueueDodger(QueueDodger.Callback callback)
    {
      this.callback = callback;
    }

    public QueueDodger(TypedObject result)
    {
      this.SetFields<QueueDodger>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<QueueDodger>(this, result);
      this.callback(this);
    }

    public delegate void Callback(QueueDodger result);
  }
}
