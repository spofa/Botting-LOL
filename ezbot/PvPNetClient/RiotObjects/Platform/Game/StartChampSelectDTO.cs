// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.StartChampSelectDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class StartChampSelectDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.game.StartChampSelectDTO";
    private StartChampSelectDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("invalidPlayers")]
    public List<object> InvalidPlayers { get; set; }

    public StartChampSelectDTO()
    {
    }

    public StartChampSelectDTO(StartChampSelectDTO.Callback callback)
    {
      this.callback = callback;
    }

    public StartChampSelectDTO(TypedObject result)
    {
      this.SetFields<StartChampSelectDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<StartChampSelectDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(StartChampSelectDTO result);
  }
}
