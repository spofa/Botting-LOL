// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RTMPSEncoder
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace PvPNetClient
{
  public class RTMPSEncoder
  {
    public long startTime = (long) DateTime.Now.TimeOfDay.TotalMilliseconds;

    public byte[] AddHeaders(byte[] data)
    {
      List<byte> byteList = new List<byte>();
      byteList.Add((byte) 3);
      long num = (long) DateTime.Now.TimeOfDay.TotalMilliseconds - this.startTime;
      byteList.Add((byte) ((num & 16711680L) >> 16));
      byteList.Add((byte) ((num & 65280L) >> 8));
      byteList.Add((byte) ((ulong) num & (ulong) byte.MaxValue));
      byteList.Add((byte) ((data.Length & 16711680) >> 16));
      byteList.Add((byte) ((data.Length & 65280) >> 8));
      byteList.Add((byte) (data.Length & (int) byte.MaxValue));
      byteList.Add((byte) 17);
      byteList.Add((byte) 0);
      byteList.Add((byte) 0);
      byteList.Add((byte) 0);
      byteList.Add((byte) 0);
      for (int index = 0; index < data.Length; ++index)
      {
        byteList.Add(data[index]);
        if (index % 128 == (int) sbyte.MaxValue && index != data.Length - 1)
          byteList.Add((byte) 195);
      }
      byte[] numArray = new byte[byteList.Count];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = byteList[index];
      return numArray;
    }

    public byte[] EncodeConnect(Dictionary<string, object> paramaters)
    {
      List<byte> ret = new List<byte>();
      this.WriteStringAMF0(ret, "connect");
      this.WriteIntAMF0(ret, 1);
      ret.Add((byte) 17);
      ret.Add((byte) 9);
      this.WriteAssociativeArray(ret, paramaters);
      ret.Add((byte) 1);
      ret.Add((byte) 0);
      this.WriteStringAMF0(ret, "nil");
      this.WriteStringAMF0(ret, "");
      TypedObject typedObject = new TypedObject("flex.messaging.messages.CommandMessage");
      typedObject.Add("operation", (object) 5);
      typedObject.Add("correlationId", (object) "");
      typedObject.Add("timestamp", (object) 0);
      typedObject.Add("messageId", (object) RTMPSEncoder.RandomUID());
      typedObject.Add("body", (object) new TypedObject((string) null));
      typedObject.Add("destination", (object) "");
      typedObject.Add("headers", (object) new Dictionary<string, object>()
      {
        {
          "DSMessagingVersion",
          (object) 1.0
        },
        {
          "DSId",
          (object) "my-rtmps"
        }
      });
      typedObject.Add("clientId", (object) null);
      typedObject.Add("timeToLive", (object) 0);
      ret.Add((byte) 17);
      this.Encode(ret, (object) typedObject);
      byte[] data = new byte[ret.Count];
      for (int index = 0; index < data.Length; ++index)
        data[index] = ret[index];
      byte[] numArray = this.AddHeaders(data);
      numArray[7] = (byte) 20;
      return numArray;
    }

    public byte[] EncodeInvoke(int id, object data)
    {
      List<byte> ret = new List<byte>();
      ret.Add((byte) 0);
      ret.Add((byte) 5);
      this.WriteIntAMF0(ret, id);
      ret.Add((byte) 5);
      ret.Add((byte) 17);
      this.Encode(ret, data);
      byte[] data1 = new byte[ret.Count];
      for (int index = 0; index < data1.Length; ++index)
        data1[index] = ret[index];
      return this.AddHeaders(data1);
    }

    public byte[] Encode(object obj)
    {
      List<byte> ret = new List<byte>();
      this.Encode(ret, obj);
      byte[] numArray = new byte[ret.Count];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = ret[index];
      return numArray;
    }

    public void Encode(List<byte> ret, object obj)
    {
      if (obj == null)
        ret.Add((byte) 1);
      else if (obj is bool)
      {
        if ((bool) obj)
          ret.Add((byte) 3);
        else
          ret.Add((byte) 2);
      }
      else if (obj is int)
      {
        ret.Add((byte) 4);
        this.WriteInt(ret, (int) obj);
      }
      else if (obj is double)
      {
        ret.Add((byte) 5);
        this.WriteDouble(ret, (double) obj);
      }
      else if (obj is string)
      {
        ret.Add((byte) 6);
        this.WriteString(ret, (string) obj);
      }
      else if (obj is DateTime)
      {
        ret.Add((byte) 8);
        this.WriteDate(ret, (DateTime) obj);
      }
      else if (obj is byte[])
      {
        ret.Add((byte) 12);
        this.WriteByteArray(ret, (byte[]) obj);
      }
      else if (obj is object[])
      {
        ret.Add((byte) 9);
        this.WriteArray(ret, (object[]) obj);
      }
      else if (obj is TypedObject)
      {
        ret.Add((byte) 10);
        this.WriteObject(ret, (TypedObject) obj);
      }
      else
      {
        if (!(obj is Dictionary<string, object>))
          throw new Exception("Unexpected object type: " + obj.GetType().FullName);
        ret.Add((byte) 9);
        this.WriteAssociativeArray(ret, (Dictionary<string, object>) obj);
      }
    }

    private void WriteInt(List<byte> ret, int val)
    {
      if (val < 0 || val >= 2097152)
      {
        ret.Add((byte) (val >> 22 & (int) sbyte.MaxValue | 128));
        ret.Add((byte) (val >> 15 & (int) sbyte.MaxValue | 128));
        ret.Add((byte) (val >> 8 & (int) sbyte.MaxValue | 128));
        ret.Add((byte) (val & (int) byte.MaxValue));
      }
      else
      {
        if (val >= 16384)
          ret.Add((byte) (val >> 14 & (int) sbyte.MaxValue | 128));
        if (val >= 128)
          ret.Add((byte) (val >> 7 & (int) sbyte.MaxValue | 128));
        ret.Add((byte) (val & (int) sbyte.MaxValue));
      }
    }

    private void WriteDouble(List<byte> ret, double val)
    {
      if (double.IsNaN(val))
      {
        ret.Add((byte) 127);
        ret.Add(byte.MaxValue);
        ret.Add(byte.MaxValue);
        ret.Add(byte.MaxValue);
        ret.Add((byte) 224);
        ret.Add((byte) 0);
        ret.Add((byte) 0);
        ret.Add((byte) 0);
      }
      else
      {
        byte[] bytes = BitConverter.GetBytes(val);
        for (int index = bytes.Length - 1; index >= 0; --index)
          ret.Add(bytes[index]);
      }
    }

    private void WriteString(List<byte> ret, string val)
    {
      byte[] bytes;
      try
      {
        bytes = new UTF8Encoding().GetBytes(val);
      }
      catch (Exception ex)
      {
        throw new Exception("Unable to encode string as UTF-8: " + val + (object) '\n' + ex.Message);
      }
      this.WriteInt(ret, bytes.Length << 1 | 1);
      foreach (byte num in bytes)
        ret.Add(num);
    }

    private void WriteDate(List<byte> ret, DateTime val)
    {
      ret.Add((byte) 1);
      this.WriteDouble(ret, val.TimeOfDay.TotalMilliseconds);
    }

    private void WriteArray(List<byte> ret, object[] val)
    {
      this.WriteInt(ret, val.Length << 1 | 1);
      ret.Add((byte) 1);
      foreach (object obj in val)
        this.Encode(ret, obj);
    }

    private void WriteAssociativeArray(List<byte> ret, Dictionary<string, object> val)
    {
      ret.Add((byte) 1);
      foreach (string key in val.Keys)
      {
        this.WriteString(ret, key);
        this.Encode(ret, val[key]);
      }
      ret.Add((byte) 1);
    }

    private void WriteObject(List<byte> ret, TypedObject val)
    {
      if (val.type == null || val.type.Equals(""))
      {
        ret.Add((byte) 11);
        ret.Add((byte) 1);
        foreach (string key in val.Keys)
        {
          this.WriteString(ret, key);
          this.Encode(ret, val[key]);
        }
        ret.Add((byte) 1);
      }
      else if (val.type.Equals("flex.messaging.io.ArrayCollection"))
      {
        ret.Add((byte) 7);
        this.WriteString(ret, val.type);
        this.Encode(ret, val["array"]);
      }
      else
      {
        this.WriteInt(ret, val.Count << 4 | 3);
        this.WriteString(ret, val.type);
        List<string> stringList = new List<string>();
        foreach (string key in val.Keys)
        {
          this.WriteString(ret, key);
          stringList.Add(key);
        }
        foreach (string index in stringList)
          this.Encode(ret, val[index]);
      }
    }

    private void WriteByteArray(List<byte> ret, byte[] val)
    {
      throw new NotImplementedException("Encoding byte arrays is not implemented");
    }

    private void WriteIntAMF0(List<byte> ret, int val)
    {
      ret.Add((byte) 0);
      byte[] bytes = BitConverter.GetBytes((double) val);
      for (int index = bytes.Length - 1; index >= 0; --index)
        ret.Add(bytes[index]);
    }

    private void WriteStringAMF0(List<byte> ret, string val)
    {
      byte[] bytes;
      try
      {
        bytes = new UTF8Encoding().GetBytes(val);
      }
      catch (Exception ex)
      {
        throw new Exception("Unable to encode string as UTF-8: " + val + (object) '\n' + ex.Message);
      }
      ret.Add((byte) 2);
      ret.Add((byte) ((bytes.Length & 65280) >> 8));
      ret.Add((byte) (bytes.Length & (int) byte.MaxValue));
      foreach (byte num in bytes)
        ret.Add(num);
    }

    public static string RandomUID()
    {
      Random random = new Random();
      byte[] buffer = new byte[16];
      random.NextBytes(buffer);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < buffer.Length; ++index)
      {
        if (index == 4 || index == 6 || (index == 8 || index == 10))
          stringBuilder.Append('-');
        stringBuilder.AppendFormat("{0:X2}", (object) buffer[index]);
      }
      return stringBuilder.ToString();
    }
  }
}
