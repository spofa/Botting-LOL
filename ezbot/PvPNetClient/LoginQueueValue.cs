// Decompiled with JetBrains decompiler
// Type: PvPNetClient.LoginQueueValue
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient
{
  public class LoginQueueValue : Attribute
  {
    private string _value;

    public string Value
    {
      get
      {
        return this._value;
      }
    }

    public LoginQueueValue(string value)
    {
      this._value = value;
    }
  }
}
