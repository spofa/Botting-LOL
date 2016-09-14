// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RegionInfo
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;

namespace PvPNetClient
{
  public static class RegionInfo
  {
    public static string GetServerValue(Enum value)
    {
      string str = (string) null;
      ServerValue[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (ServerValue), false) as ServerValue[];
      if (customAttributes.Length > 0)
        str = customAttributes[0].Value;
      return str;
    }

    public static string GetLoginQueueValue(Enum value)
    {
      string str = (string) null;
      LoginQueueValue[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (LoginQueueValue), false) as LoginQueueValue[];
      if (customAttributes.Length > 0)
        str = customAttributes[0].Value;
      return str;
    }

    public static string GetLocaleValue(Enum value)
    {
      string str = (string) null;
      LocaleValue[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (LocaleValue), false) as LocaleValue[];
      if (customAttributes.Length > 0)
        str = customAttributes[0].Value;
      return str;
    }

    public static bool GetUseGarenaValue(Enum value)
    {
      bool flag = false;
      UseGarenaValue[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (UseGarenaValue), false) as UseGarenaValue[];
      if (customAttributes.Length > 0)
        flag = customAttributes[0].Value;
      return flag;
    }
  }
}
