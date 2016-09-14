// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.Platform.Statistics.RawStatDTO
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects.Platform.Statistics
{
  public class RawStatDTO : RiotGamesObject
  {
    private string type = "com.riotgames.platform.statistics.RawStatDTO";
    private RawStatDTO.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    [InternalName("value")]
    public double Value { get; set; }

    [InternalName("statTypeName")]
    public string StatTypeName { get; set; }

    public RawStatDTO()
    {
    }

    public RawStatDTO(RawStatDTO.Callback callback)
    {
      this.callback = callback;
    }

    public RawStatDTO(TypedObject result)
    {
      this.SetFields<RawStatDTO>(this, result);
    }

    public override void DoCallback(TypedObject result)
    {
      this.SetFields<RawStatDTO>(this, result);
      this.callback(this);
    }

    public delegate void Callback(RawStatDTO result);
  }
}
