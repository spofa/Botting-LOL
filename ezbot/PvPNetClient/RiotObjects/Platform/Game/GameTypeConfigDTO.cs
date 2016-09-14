// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.GameTypeConfigDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class GameTypeConfigDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.GameTypeConfigDTO";
    private GameTypeConfigDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("id")]
    public int Id { get; set; }

    [InternalName("allowTrades")]
    public bool AllowTrades { get; set; }

    [InternalName("name")]
    public string Name { get; set; }

    [InternalName("mainPickTimerDuration")]
    public int MainPickTimerDuration { get; set; }

    [InternalName("exclusivePick")]
    public bool ExclusivePick { get; set; }

    [InternalName("pickMode")]
    public string PickMode { get; set; }

    [InternalName("maxAllowableBans")]
    public int MaxAllowableBans { get; set; }

    [InternalName("banTimerDuration")]
    public int BanTimerDuration { get; set; }

    [InternalName("postPickTimerDuration")]
    public int PostPickTimerDuration { get; set; }

    public GameTypeConfigDTO()
    {
    }

    public GameTypeConfigDTO(GameTypeConfigDTO.Callback callback)
    {
      this.callback = callback;
    }

    public GameTypeConfigDTO(TypedObject result)
    {
      this.SetFields<GameTypeConfigDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<GameTypeConfigDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(GameTypeConfigDTO result);
  }
}
