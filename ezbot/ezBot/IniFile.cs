// Decompiled with JetBrains decompiler
// Type: ezBot.IniFile
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Runtime.InteropServices;
using System.Text;

namespace ezBot
{
  public class IniFile
  {
    public string path;

    public IniFile(string INIPath)
    {
      this.path = INIPath;
    }

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    public void Write(string Section, string Key, string Value)
    {
      IniFile.WritePrivateProfileString(Section, Key, Value, this.path);
    }

    public string Read(string Section, string Key)
    {
      StringBuilder retVal = new StringBuilder((int) byte.MaxValue);
      IniFile.GetPrivateProfileString(Section, Key, "", retVal, (int) byte.MaxValue, this.path);
      return retVal.ToString();
    }
  }
}
