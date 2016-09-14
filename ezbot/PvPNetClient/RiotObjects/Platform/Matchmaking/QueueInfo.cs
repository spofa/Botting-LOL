// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Matchmaking.QueueInfo
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Matchmaking
{
  public class QueueInfo : RiotGamesObject
  {
    private string type = "com.riotgames.platform.matchmaking.QueueInfo";
    private QueueInfo.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("waitTime")]
    public double WaitTime { get; set; }

    [InternalName("queueId")]
    public double QueueId { get; set; }

    [InternalName("queueLength")]
    public int QueueLength { get; set; }

    public QueueInfo()
    {
    }

    public QueueInfo(QueueInfo.Callback callback)
    {
      this.callback = callback;
    }

    public QueueInfo(TypedObject result)
    {
      this.SetFields<QueueInfo>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<QueueInfo>(this, result);
      this.callback(this);
    }

    public delegate void Callback(QueueInfo result);
  }
}
