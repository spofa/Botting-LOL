// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Matchmaking.MatchingThrottleConfig
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Matchmaking
{
  public class MatchingThrottleConfig : RiotGamesObject
  {
    private string type = "com.riotgames.platform.matchmaking.MatchingThrottleConfig";
    private MatchingThrottleConfig.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("limit")]
    public double Limit { get; set; }

    [InternalName("matchingThrottleProperties")]
    public List<object> MatchingThrottleProperties { get; set; }

    [InternalName("cacheName")]
    public string CacheName { get; set; }

    public MatchingThrottleConfig()
    {
    }

    public MatchingThrottleConfig(MatchingThrottleConfig.Callback callback)
    {
      this.callback = callback;
    }

    public MatchingThrottleConfig(TypedObject result)
    {
      this.SetFields<MatchingThrottleConfig>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<MatchingThrottleConfig>(this, result);
      this.callback(this);
    }

    public delegate void Callback(MatchingThrottleConfig result);
  }
}
