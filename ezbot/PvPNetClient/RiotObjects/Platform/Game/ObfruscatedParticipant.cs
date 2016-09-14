// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.ObfruscatedParticipant
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class ObfruscatedParticipant : Participant
  {
    private string type = "com.riotgames.platform.game.ObfruscatedParticipant";
    private ObfruscatedParticipant.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("badges")]
    public int Badges { get; set; }

    [InternalName("index")]
    public int Index { get; set; }

    [InternalName("clientInSynch")]
    public bool ClientInSynch { get; set; }

    [InternalName("gameUniqueId")]
    public int GameUniqueId { get; set; }

    [InternalName("pickMode")]
    public int PickMode { get; set; }

    public ObfruscatedParticipant()
    {
    }

    public ObfruscatedParticipant(ObfruscatedParticipant.Callback callback)
    {
      this.callback = callback;
    }

    public ObfruscatedParticipant(TypedObject result)
    {
      this.SetFields<ObfruscatedParticipant>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<ObfruscatedParticipant>(this, result);
      this.callback(this);
    }

    public delegate void Callback(ObfruscatedParticipant result);
  }
}
