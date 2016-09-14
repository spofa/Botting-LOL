// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.ChampionBanInfoDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class ChampionBanInfoDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.ChampionBanInfoDTO";
    private ChampionBanInfoDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("enemyOwned")]
    public bool EnemyOwned { get; set; }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    [InternalName("owned")]
    public bool Owned { get; set; }

    public ChampionBanInfoDTO(ChampionBanInfoDTO.Callback callback)
    {
      this.callback = callback;
    }

    public ChampionBanInfoDTO(TypedObject result)
    {
      this.SetFields<ChampionBanInfoDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ChampionBanInfoDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ChampionBanInfoDTO result);
  }
}
