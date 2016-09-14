﻿// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Systemstate.ClientSystemStatesNotification
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Systemstate
{
  public class ClientSystemStatesNotification : RiotGamesObject
  {
    private string type = "com.riotgames.platform.systemstate.ClientSystemStatesNotification";
    private ClientSystemStatesNotification.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("championTradeThroughLCDS")]
    public bool ChampionTradeThroughLCDS { get; set; }

    [InternalName("practiceGameEnabled")]
    public bool PracticeGameEnabled { get; set; }

    [InternalName("advancedTutorialEnabled")]
    public bool AdvancedTutorialEnabled { get; set; }

    [InternalName("minNumPlayersForPracticeGame")]
    public int MinNumPlayersForPracticeGame { get; set; }

    [InternalName("practiceGameTypeConfigIdList")]
    public int[] PracticeGameTypeConfigIdList { get; set; }

    [InternalName("freeToPlayChampionIdList")]
    public int[] FreeToPlayChampionIdList { get; set; }

    [InternalName("inactiveChampionIdList")]
    public object[] InactiveChampionIdList { get; set; }

    [InternalName("inactiveSpellIdList")]
    public int[] InactiveSpellIdList { get; set; }

    [InternalName("inactiveTutorialSpellIdList")]
    public int[] InactiveTutorialSpellIdList { get; set; }

    [InternalName("inactiveClassicSpellIdList")]
    public int[] InactiveClassicSpellIdList { get; set; }

    [InternalName("inactiveOdinSpellIdList")]
    public int[] InactiveOdinSpellIdList { get; set; }

    [InternalName("inactiveAramSpellIdList")]
    public int[] InactiveAramSpellIdList { get; set; }

    [InternalName("enabledQueueIdsList")]
    public int[] EnabledQueueIdsList { get; set; }

    [InternalName("unobtainableChampionSkinIDList")]
    public int[] UnobtainableChampionSkinIDList { get; set; }

    [InternalName("archivedStatsEnabled")]
    public bool ArchivedStatsEnabled { get; set; }

    [InternalName("queueThrottleDTO")]
    public Dictionary<string, object> QueueThrottleDTO { get; set; }

    [InternalName("gameMapEnabledDTOList")]
    public List<Dictionary<string, object>> GameMapEnabledDTOList { get; set; }

    [InternalName("storeCustomerEnabled")]
    public bool StoreCustomerEnabled { get; set; }

    [InternalName("socialIntegrationEnabled")]
    public bool SocialIntegrationEnabled { get; set; }

    [InternalName("runeUniquePerSpellBook")]
    public bool RuneUniquePerSpellBook { get; set; }

    [InternalName("tribunalEnabled")]
    public bool TribunalEnabled { get; set; }

    [InternalName("observerModeEnabled")]
    public bool ObserverModeEnabled { get; set; }

    [InternalName("spectatorSlotLimit")]
    public int SpectatorSlotLimit { get; set; }

    [InternalName("clientHeartBeatRateSeconds")]
    public int ClientHeartBeatRateSeconds { get; set; }

    [InternalName("observableGameModes")]
    public string[] ObservableGameModes { get; set; }

    [InternalName("observableCustomGameModes")]
    public string ObservableCustomGameModes { get; set; }

    [InternalName("teamServiceEnabled")]
    public bool TeamServiceEnabled { get; set; }

    [InternalName("leagueServiceEnabled")]
    public bool LeagueServiceEnabled { get; set; }

    [InternalName("modularGameModeEnabled")]
    public bool ModularGameModeEnabled { get; set; }

    [InternalName("riotDataServiceDataSendProbability")]
    public int RiotDataServiceDataSendProbability { get; set; }

    [InternalName("displayPromoGamesPlayedEnabled")]
    public bool DisplayPromoGamesPlayedEnabled { get; set; }

    [InternalName("masteryPageOnServer")]
    public bool MasteryPageOnServer { get; set; }

    [InternalName("maxMasteryPagesOnServer")]
    public int MaxMasteryPagesOnServer { get; set; }

    [InternalName("tournamentSendStatsEnabled")]
    public bool TournamentSendStatsEnabled { get; set; }

    [InternalName("replayServiceAddress")]
    public string ReplayServiceAddress { get; set; }

    [InternalName("kudosEnabled")]
    public bool KudosEnabled { get; set; }

    [InternalName("buddyNotesEnabled")]
    public bool BuddyNotesEnabled { get; set; }

    [InternalName("localeSpecificChatRoomsEnabled")]
    public bool LocaleSpecificChatRoomsEnabled { get; set; }

    [InternalName("replaySystemStates")]
    public Dictionary<string, object> ReplaySystemStates { get; set; }

    [InternalName("sendFeedbackEventsEnabled")]
    public bool SendFeedbackEventsEnabled { get; set; }

    [InternalName("knownGeographicGameServerRegions")]
    public string[] KnownGeographicGameServerRegions { get; set; }

    [InternalName("leaguesDecayMessagingEnabled")]
    public bool LeaguesDecayMessagingEnabled { get; set; }

    public ClientSystemStatesNotification()
    {
    }

    public ClientSystemStatesNotification(ClientSystemStatesNotification.Callback callback)
    {
      this.callback = callback;
    }

    public ClientSystemStatesNotification(TypedObject result)
    {
      this.SetFields<ClientSystemStatesNotification>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ClientSystemStatesNotification>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ClientSystemStatesNotification result);
  }
}
