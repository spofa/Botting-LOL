// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Game.Participant
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Game
{
  public class Participant : RiotGamesObject
  {
    private Participant.Callback callback;

    public Participant()
    {
    }

    public Participant(Participant.Callback callback)
    {
      this.callback = callback;
    }

    public Participant(TypedObject result)
    {
      this.SetFields<Participant>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<Participant>(this, result);
      this.callback(this);
    }

    public delegate void Callback(Participant result);
  }
}
