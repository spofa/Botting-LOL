// Decompiled with JetBrains decompiler
// Type: PvPNetClient.GameType
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient
{
  public enum GameType
  {
    [StringValue("RANKED_TEAM_GAME")] RankedTeamGame,
    [StringValue("RANKED_GAME")] RankedGame,
    [StringValue("NORMAL_GAME")] NormalGame,
    [StringValue("CUSTOM_GAME")] CustomGame,
    [StringValue("TUTORIAL_GAME")] TutorialGame,
    [StringValue("PRACTICE_GAME")] PracticeGame,
    [StringValue("RANKED_GAME_SOLO")] RankedGameSolo,
    [StringValue("COOP_VS_AI")] CoopVsAi,
    [StringValue("RANKED_GAME_PREMADE")] RankedGamePremade,
  }
}
