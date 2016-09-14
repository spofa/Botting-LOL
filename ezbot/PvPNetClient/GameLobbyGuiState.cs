// Decompiled with JetBrains decompiler
// Type: PvPNetClient.GameLobbyGuiState
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient
{
  [Flags]
  public enum GameLobbyGuiState
  {
    IDLE = 1,
    TEAM_SELECT = 2,
    CHAMP_SELECT = 4,
    POST_CHAMP_SELECT = 8,
    PRE_CHAMP_SELECT = 16,
    START_REQUESTED = 32,
    GAME_START_CLIENT = 64,
    GameClientConnectedToServer = 128,
    IN_PROGRESS = 256,
    IN_QUEUE = 512,
    POST_GAME = 1024,
    TERMINATED = 2048,
    TERMINATED_IN_ERROR = 4096,
    CHAMP_SELECT_CLIENT = 8192,
    GameReconnect = 16384,
    GAME_IN_PROGRESS = 32768,
    JOINING_CHAMP_SELECT = 65536,
    DISCONNECTED = 131072,
  }
}
