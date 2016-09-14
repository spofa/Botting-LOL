// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.PlayerChampionSelectionDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class PlayerChampionSelectionDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.PlayerChampionSelectionDTO";
    private PlayerChampionSelectionDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [InternalName("spell2Id")]
    public double Spell2Id { get; set; }

    [InternalName("selectedSkinIndex")]
    public int SelectedSkinIndex { get; set; }

    [InternalName("championId")]
    public int ChampionId { get; set; }

    [InternalName("spell1Id")]
    public double Spell1Id { get; set; }

    public PlayerChampionSelectionDTO()
    {
    }

    public PlayerChampionSelectionDTO(PlayerChampionSelectionDTO.Callback callback)
    {
      this.callback = callback;
    }

    public PlayerChampionSelectionDTO(TypedObject result)
    {
      this.SetFields<PlayerChampionSelectionDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PlayerChampionSelectionDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PlayerChampionSelectionDTO result);
  }
}
