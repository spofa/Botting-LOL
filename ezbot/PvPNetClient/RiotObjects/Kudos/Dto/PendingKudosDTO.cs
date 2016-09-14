// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Kudos.Dto.PendingKudosDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Kudos.Dto
{
  public class PendingKudosDTO : RiotGamesObject
  {
    private string type = "com.riotgames.kudos.dto.PendingKudosDTO";
    private PendingKudosDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("pendingCounts")]
    public int[] PendingCounts { get; set; }

    public PendingKudosDTO()
    {
    }

    public PendingKudosDTO(PendingKudosDTO.Callback callback)
    {
      this.callback = callback;
    }

    public PendingKudosDTO(TypedObject result)
    {
      this.SetFields<PendingKudosDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<PendingKudosDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(PendingKudosDTO result);
  }
}
