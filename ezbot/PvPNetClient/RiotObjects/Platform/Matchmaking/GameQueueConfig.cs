// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Matchmaking.GameQueueConfig
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Matchmaking
{
  public class GameQueueConfig : RiotGamesObject
  {
    private string type = "com.riotgames.platform.matchmaking.GameQueueConfig";
    private GameQueueConfig.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("blockedMinutesThreshold")]
    public int BlockedMinutesThreshold { get; set; }

    [InternalName("minimumParticipantListSize")]
    public int MinimumParticipantListSize { get; set; }

    [InternalName("ranked")]
    public bool Ranked { get; set; }

    [InternalName("maxLevel")]
    public int MaxLevel { get; set; }

    [InternalName("minLevel")]
    public int MinLevel { get; set; }

    [InternalName("gameTypeConfigId")]
    public int GameTypeConfigId { get; set; }

    [InternalName("thresholdEnabled")]
    public bool ThresholdEnabled { get; set; }

    [InternalName("queueState")]
    public string QueueState { get; set; }

    [InternalName("type")]
    public string Type { get; set; }

    [InternalName("cacheName")]
    public string CacheName { get; set; }

    [InternalName("id")]
    public double Id { get; set; }

    [InternalName("queueBonusKey")]
    public string QueueBonusKey { get; set; }

    [InternalName("queueStateString")]
    public string QueueStateString { get; set; }

    [InternalName("pointsConfigKey")]
    public string PointsConfigKey { get; set; }

    [InternalName("teamOnly")]
    public bool TeamOnly { get; set; }

    [InternalName("minimumQueueDodgeDelayTime")]
    public int MinimumQueueDodgeDelayTime { get; set; }

    [InternalName("supportedMapIds")]
    public List<int> SupportedMapIds { get; set; }

    [InternalName("gameMode")]
    public string GameMode { get; set; }

    [InternalName("typeString")]
    public string TypeString { get; set; }

    [InternalName("numPlayersPerTeam")]
    public int NumPlayersPerTeam { get; set; }

    [InternalName("maximumParticipantListSize")]
    public int MaximumParticipantListSize { get; set; }

    [InternalName("disallowFreeChampions")]
    public bool DisallowFreeChampions { get; set; }

    [InternalName("mapSelectionAlgorithm")]
    public string MapSelectionAlgorithm { get; set; }

    [InternalName("thresholdSize")]
    public double ThresholdSize { get; set; }

    [InternalName("matchingThrottleConfig")]
    public MatchingThrottleConfig MatchingThrottleConfig { get; set; }

    public GameQueueConfig()
    {
    }

    public GameQueueConfig(GameQueueConfig.Callback callback)
    {
      this.callback = callback;
    }

    public GameQueueConfig(TypedObject result)
    {
      this.SetFields<GameQueueConfig>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<GameQueueConfig>(this, result);
      this.callback(this);
    }

    public delegate void Callback(GameQueueConfig result);
  }
}
