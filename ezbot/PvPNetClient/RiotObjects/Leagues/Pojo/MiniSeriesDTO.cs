// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Leagues.Pojo.MiniSeriesDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Leagues.Pojo
{
  public class MiniSeriesDTO : RiotGamesObject
  {
    private string type = "com.riotgames.leagues.pojo.MiniSeriesDTO";
    private MiniSeriesDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("progress")]
    public string Progress { get; set; }

    [InternalName("target")]
    public int Target { get; set; }

    [InternalName("losses")]
    public int Losses { get; set; }

    [InternalName("timeLeftToPlayMillis")]
    public double TimeLeftToPlayMillis { get; set; }

    [InternalName("wins")]
    public int Wins { get; set; }

    public MiniSeriesDTO()
    {
    }

    public MiniSeriesDTO(MiniSeriesDTO.Callback callback)
    {
      this.callback = callback;
    }

    public MiniSeriesDTO(TypedObject result)
    {
      this.SetFields<MiniSeriesDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<MiniSeriesDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(MiniSeriesDTO result);
  }
}
