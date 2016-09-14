// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Gameinvite.Contract.LobbyStatus
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient.RiotObjects.Platform.Gameinvite.Contract
{
  public class LobbyStatus : RiotGamesObject
  {
    private string type = "com.riotgames.platform.gameinvite.contract.LobbyStatus";
    private LobbyStatus.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("gameMetaData")]
    public Dictionary<string, object> GameMetaData { get; set; }

    public LobbyStatus(LobbyStatus.Callback callback)
    {
      this.callback = callback;
    }

    public LobbyStatus(TypedObject result)
    {
      this.SetFields<LobbyStatus>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<LobbyStatus>(this, result);
      this.callback(this);
    }

    public delegate void Callback(LobbyStatus result);
  }
}
