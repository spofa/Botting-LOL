// Decompiled with JetBrains decompiler
// Type: PvPNetClient.StringEnum
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient
{
  public static class StringEnum
  {
    public static string GetStringValue(Enum value)
    {
      string str = (string) null;
      StringValue[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (StringValue), false) as StringValue[];
      if (customAttributes.Length > 0)
        str = customAttributes[0].Value;
      return str;
    }
  }
}
