// Decompiled with JetBrains decompiler
// Type: PvPNetClient.TypedObject
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace PvPNetClient
{
  public class TypedObject : Dictionary<string, object>
  {
    public string type;

    public TypedObject()
    {
      this.type = (string) null;
    }

    public TypedObject(string type)
    {
      this.type = type;
    }

    public static TypedObject MakeArrayCollection(object[] data)
    {
      TypedObject typedObject = new TypedObject("flex.messaging.io.ArrayCollection");
      typedObject.Add("array", (object) data);
      return typedObject;
    }

    public TypedObject GetTO(string key)
    {
      if (this.ContainsKey(key) && this[key] is TypedObject)
        return (TypedObject) this[key];
      return (TypedObject) null;
    }

    public string GetString(string key)
    {
      return (string) this[key];
    }

    public int? GetInt(string key)
    {
      object obj = this[key];
      if (obj == null)
        return new int?();
      if (obj is int)
        return new int?((int) obj);
      return new int?(Convert.ToInt32((double) obj));
    }

    public double? GetDouble(string key)
    {
      object obj = this[key];
      if (obj == null)
        return new double?();
      if (obj is double)
        return new double?((double) obj);
      return new double?(Convert.ToDouble((int) obj));
    }

    public bool GetBool(string key)
    {
      return (bool) this[key];
    }

    public object[] GetArray(string key)
    {
      if (this[key] is TypedObject && this.GetTO(key).type.Equals("flex.messaging.io.ArrayCollection"))
        return (object[]) this.GetTO(key)["array"];
      return (object[]) this[key];
    }

    public override string ToString()
    {
      if (this.type == null)
        return base.ToString();
      if (this.type.Equals("flex.messaging.io.ArrayCollection"))
      {
        StringBuilder stringBuilder = new StringBuilder();
        object[] objArray = (object[]) this["array"];
        stringBuilder.Append("ArrayCollection[");
        for (int index = 0; index < objArray.Length; ++index)
        {
          stringBuilder.Append(objArray[index]);
          if (index < objArray.Length - 1)
            stringBuilder.Append(", ");
        }
        stringBuilder.Append(']');
        return stringBuilder.ToString();
      }
      string str = "";
      foreach (KeyValuePair<string, object> keyValuePair in (Dictionary<string, object>) this)
        str = str + keyValuePair.Key + " : " + keyValuePair.Value + "\n";
      return str + this.type + ":" + base.ToString();
    }
  }
}
