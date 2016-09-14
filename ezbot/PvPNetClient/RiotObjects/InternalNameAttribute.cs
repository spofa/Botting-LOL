// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RiotObjects.InternalNameAttribute
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient.RiotObjects
{
  public class InternalNameAttribute : Attribute
  {
    public string Name { get; set; }

    public InternalNameAttribute(string name)
    {
      this.Name = name;
    }
  }
}
