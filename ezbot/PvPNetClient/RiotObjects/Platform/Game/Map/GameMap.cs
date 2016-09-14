﻿// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.Map.GameMap
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game.Map
{
  public class GameMap : RiotGamesObject
  {
    public static GameMap SummonersRift = new GameMap()
    {
      Description = "The oldest and most venerated Field of Justice is known as Summoner's Rift.  This battleground is known for the constant conflicts fought between two opposing groups of Summoners.  Traverse down one of three different paths in order to attack your enemy at their weakest point.  Work with your allies to siege the enemy base and destroy their Headquarters!",
      MapId = 1,
      DisplayName = "Summoner's Rift",
      MinCustomPlayers = 1,
      Name = "SummonersRift",
      TotalPlayers = 10
    };
    public static GameMap TheTwistedTreeline = new GameMap()
    {
      Description = "Deep in the Shadow Isles lies a ruined city shattered by magical disaster. Those who venture inside the ruins and wander through the Twisted Treeline seldom return, but those who do tell tales of horrific creatures and the vengeful dead.",
      MapId = 10,
      DisplayName = "The Twisted Treeline",
      MinCustomPlayers = 1,
      Name = "TwistedTreeline",
      TotalPlayers = 6
    };
    public static GameMap HowlingAbyss = new GameMap()
    {
      Description = "The Howling Abyss is a bottomless crevasse located in the coldest, cruelest, part of the Freljord. Legends say that, long ago, a great battle took place here on the narrow bridge spanning this chasm. No one remembers who fought here, or why, but it is said that if you listen carefully to the wind you can still hear the cries of the vanquished tossed howling into the Abyss.",
      MapId = 12,
      DisplayName = "Howling Abyss",
      MinCustomPlayers = 1,
      Name = "HowlingAbyss",
      TotalPlayers = 10
    };
    private string type = "com.riotgames.platform.game.map.GameMap";
    private GameMap.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("displayName")]
    public string DisplayName { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("mapId")]
    public int MapId { get; set; }

    [InternalName("minCustomPlayers")]
    public int MinCustomPlayers { get; set; }

    [InternalName("totalPlayers")]
    public int TotalPlayers { get; set; }

    [InternalName("description")]
    public string Description { get; set; }

    public GameMap()
    {
    }

    public GameMap(GameMap.Callback callback)
    {
      this.callback = callback;
    }

    public GameMap(TypedObject result)
    {
      this.SetFields<GameMap>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<GameMap>(this, result);
      this.callback(this);
    }

    public delegate void Callback(GameMap result);
  }
}
