// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.UnclassedObject
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient.RiotObjects
{
  public class UnclassedObject : RiotGamesObject
  {
    private string type = "";
    private UnclassedObject.Callback callback;

    public override string TypeName
    {
      get
      {
        return this.type;
      }
    }

    public UnclassedObject(UnclassedObject.Callback callback)
    {
      this.callback = callback;
    }

    public override void DoCallback(TypedObject result)
    {
      this.callback(result);
    }

    public delegate void Callback(TypedObject result);
  }
}
