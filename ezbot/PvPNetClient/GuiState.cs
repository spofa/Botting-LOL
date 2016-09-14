// Decompiled with JetBrains decompiler
// Type: PvPNetClient.GuiState
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient
{
  [Flags]
  public enum GuiState
  {
    None = 1,
    LoggedOut = 2,
    LoggingIn = 4,
    LoggedIn = 8,
    CustomSearchGame = 16,
    CustomCreateGame = 32,
    GameLobby = 64,
  }
}
