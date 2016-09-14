// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.BannedChampion
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class BannedChampion : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.BannedChampion";
    private BannedChampion.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("pickTurn")]
    public int PickTurn { get; set; }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    [InternalName("teamId")]
    public int TeamId { get; set; }

    public BannedChampion(BannedChampion.Callback callback)
    {
      this.callback = callback;
    }

    public BannedChampion(TypedObject result)
    {
      this.SetFields<BannedChampion>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<BannedChampion>(this, result);
      this.callback(this);
    }

    public delegate void Callback(BannedChampion result);
  }
}
