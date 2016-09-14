// Decompiled with JetBrains decompiler
// Type: PvPNetClient.RTMPSDecoder
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace PvPNetClient
{
  public class RTMPSDecoder
  {
    private List<string> stringReferences = new List<string>();
    private List<object> objectReferences = new List<object>();
    private List<ClassDefinition> classDefinitions = new List<ClassDefinition>();
    private byte[] dataBuffer;
    private int dataPos;

    private void Reset()
    {
      this.stringReferences.Clear();
      this.objectReferences.Clear();
      this.classDefinitions.Clear();
    }

    public TypedObject DecodeConnect(byte[] data)
    {
      this.Reset();
      this.dataBuffer = data;
      this.dataPos = 0;
      TypedObject typedObject = new TypedObject("Invoke");
      typedObject.Add("result", this.DecodeAMF0());
      typedObject.Add("invokeId", this.DecodeAMF0());
      typedObject.Add("serviceCall", this.DecodeAMF0());
      typedObject.Add("data", this.DecodeAMF0());
      if (this.dataPos != this.dataBuffer.Length)
      {
        for (int dataPos = this.dataPos; dataPos < data.Length; ++dataPos)
        {
          if ((int) this.ReadByte() != 0)
            throw new Exception("There is other data in the buffer!");
        }
      }
      if (this.dataPos != this.dataBuffer.Length)
        throw new Exception("Did not consume entire buffer: " + (object) this.dataPos + " of " + (object) this.dataBuffer.Length);
      return typedObject;
    }

    public TypedObject DecodeInvoke(byte[] data)
    {
      this.Reset();
      this.dataBuffer = data;
      this.dataPos = 0;
      TypedObject typedObject = new TypedObject("Invoke");
      if ((int) this.dataBuffer[0] == 0)
      {
        ++this.dataPos;
        typedObject.Add("version", (object) 0);
      }
      typedObject.Add("result", this.DecodeAMF0());
      typedObject.Add("invokeId", this.DecodeAMF0());
      typedObject.Add("serviceCall", this.DecodeAMF0());
      typedObject.Add("data", this.DecodeAMF0());
      if (this.dataPos != this.dataBuffer.Length)
        throw new Exception("Did not consume entire buffer: " + (object) this.dataPos + " of " + (object) this.dataBuffer.Length);
      return typedObject;
    }

    public object Decode(byte[] data)
    {
      this.dataBuffer = data;
      this.dataPos = 0;
      object obj = this.Decode();
      if (this.dataPos != this.dataBuffer.Length)
        throw new Exception("Did not consume entire buffer: " + (object) this.dataPos + " of " + (object) this.dataBuffer.Length);
      return obj;
    }

    private object Decode()
    {
      byte num = this.ReadByte();
      switch (num)
      {
        case 0:
          throw new Exception("Undefined data type");
        case 1:
          return (object) null;
        case 2:
          return (object) false;
        case 3:
          return (object) true;
        case 4:
          return (object) this.ReadInt();
        case 5:
          return (object) this.ReadDouble();
        case 6:
          return (object) this.ReadString();
        case 7:
          return (object) this.ReadXML();
        case 8:
          return (object) this.ReadDate();
        case 9:
          return (object) this.ReadArray();
        case 10:
          return this.ReadObject();
        case 11:
          return (object) this.ReadXMLString();
        case 12:
          return (object) this.ReadByteArray();
        default:
          throw new Exception("Unexpected AMF3 data type: " + (object) num);
      }
    }

    private byte ReadByte()
    {
      byte num = this.dataBuffer[this.dataPos];
      ++this.dataPos;
      return num;
    }

    private int ReadByteAsInt()
    {
      int num = (int) this.ReadByte();
      if (num < 0)
        num += 256;
      return num;
    }

    private byte[] ReadBytes(int length)
    {
      byte[] numArray = new byte[length];
      for (int index = 0; index < length; ++index)
      {
        numArray[index] = this.dataBuffer[this.dataPos];
        ++this.dataPos;
      }
      return numArray;
    }

    private int ReadInt()
    {
      int num1 = this.ReadByteAsInt();
      if (num1 < 128)
        return num1;
      int num2 = (num1 & (int) sbyte.MaxValue) << 7;
      int num3 = this.ReadByteAsInt();
      int num4;
      if (num3 < 128)
      {
        num4 = num2 | num3;
      }
      else
      {
        int num5 = (num2 | num3 & (int) sbyte.MaxValue) << 7;
        int num6 = this.ReadByteAsInt();
        num4 = num6 >= 128 ? (num5 | num6 & (int) sbyte.MaxValue) << 8 | this.ReadByteAsInt() : num5 | num6;
      }
      int num7 = 268435456;
      return -(num4 & num7) | num4;
    }

    private double ReadDouble()
    {
      long num = 0;
      for (int index = 0; index < 8; ++index)
        num = (num << 8) + (long) this.ReadByteAsInt();
      return BitConverter.Int64BitsToDouble(num);
    }

    private string ReadString()
    {
      int num = this.ReadInt();
      bool flag = (num & 1) != 0;
      int length = num >> 1;
      if (!flag)
        return this.stringReferences[length];
      if (length == 0)
        return "";
      byte[] bytes = this.ReadBytes(length);
      string str;
      try
      {
        str = new UTF8Encoding().GetString(bytes);
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing AMF3 string from " + (object) bytes + (object) '\n' + ex.Message);
      }
      this.stringReferences.Add(str);
      return str;
    }

    private string ReadXML()
    {
      throw new NotImplementedException("Reading of XML is not implemented");
    }

    private DateTime ReadDate()
    {
      int num1 = this.ReadInt();
      bool flag = (num1 & 1) != 0;
      int num2 = num1 >> 1;
      if (!flag)
        return DateTime.MinValue;
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double) ((long) this.ReadDouble() / 1000L));
      this.objectReferences.Add((object) dateTime);
      return dateTime;
    }

    private object[] ReadArray()
    {
      int num = this.ReadInt();
      bool flag = (num & 1) != 0;
      int length = num >> 1;
      if (!flag)
        return (object[]) this.objectReferences[length];
      string str = this.ReadString();
      if (str != null && !str.Equals(""))
        throw new NotImplementedException("Associative arrays are not supported");
      object[] objArray = new object[length];
      this.objectReferences.Add((object) objArray);
      for (int index = 0; index < length; ++index)
        objArray[index] = this.Decode();
      return objArray;
    }

    private List<object> ReadList()
    {
      int num = this.ReadInt();
      bool flag = (num & 1) != 0;
      int index1 = num >> 1;
      if (!flag)
        return (List<object>) this.objectReferences[index1];
      string str = this.ReadString();
      if (str != null && !str.Equals(""))
        throw new NotImplementedException("Associative arrays are not supported");
      List<object> objectList = new List<object>();
      this.objectReferences.Add((object) objectList);
      for (int index2 = 0; index2 < index1; ++index2)
        objectList.Add(this.Decode());
      return objectList;
    }

    private object ReadObject()
    {
      int num1 = this.ReadInt();
      bool flag1 = (num1 & 1) != 0;
      int index1 = num1 >> 1;
      if (!flag1)
        return this.objectReferences[index1];
      bool flag2 = (index1 & 1) != 0;
      int index2 = index1 >> 1;
      ClassDefinition classDefinition;
      if (flag2)
      {
        classDefinition = new ClassDefinition();
        classDefinition.type = this.ReadString();
        classDefinition.externalizable = (index2 & 1) != 0;
        int num2 = index2 >> 1;
        classDefinition.dynamic = (num2 & 1) != 0;
        int num3 = num2 >> 1;
        for (int index3 = 0; index3 < num3; ++index3)
          classDefinition.members.Add(this.ReadString());
        this.classDefinitions.Add(classDefinition);
      }
      else
        classDefinition = this.classDefinitions[index2];
      TypedObject typedObject = new TypedObject(classDefinition.type);
      this.objectReferences.Add((object) typedObject);
      if (classDefinition.externalizable)
      {
        if (classDefinition.type.Equals("DSK"))
          typedObject = this.ReadDSK();
        else if (classDefinition.type.Equals("DSA"))
          typedObject = this.ReadDSA();
        else if (classDefinition.type.Equals("flex.messaging.io.ArrayCollection"))
        {
          typedObject = TypedObject.MakeArrayCollection((object[]) this.Decode());
        }
        else
        {
          if (!classDefinition.type.Equals("com.riotgames.platform.systemstate.ClientSystemStatesNotification") && !classDefinition.type.Equals("com.riotgames.platform.broadcast.BroadcastNotification"))
            throw new NotImplementedException("Externalizable not handled for " + classDefinition.type);
          int length = 0;
          for (int index3 = 0; index3 < 4; ++index3)
            length = length * 256 + this.ReadByteAsInt();
          byte[] numArray = this.ReadBytes(length);
          StringBuilder stringBuilder = new StringBuilder();
          for (int index3 = 0; index3 < numArray.Length; ++index3)
            stringBuilder.Append(Convert.ToChar(numArray[index3]));
          typedObject = new JavaScriptSerializer().Deserialize<TypedObject>(stringBuilder.ToString());
          typedObject.type = classDefinition.type;
        }
      }
      else
      {
        for (int index3 = 0; index3 < classDefinition.members.Count; ++index3)
        {
          string member = classDefinition.members[index3];
          object obj = this.Decode();
          typedObject.Add(member, obj);
        }
        if (classDefinition.dynamic)
        {
          string key;
          while ((key = this.ReadString()).Length != 0)
          {
            object obj = this.Decode();
            typedObject.Add(key, obj);
          }
        }
      }
      return (object) typedObject;
    }

    private string ReadXMLString()
    {
      throw new NotImplementedException("Reading of XML strings is not implemented");
    }

    private byte[] ReadByteArray()
    {
      int num = this.ReadInt();
      bool flag = (num & 1) != 0;
      int length = num >> 1;
      if (!flag)
        return (byte[]) this.objectReferences[length];
      byte[] numArray = this.ReadBytes(length);
      this.objectReferences.Add((object) numArray);
      return numArray;
    }

    private TypedObject ReadDSA()
    {
      TypedObject typedObject = new TypedObject("DSA");
      List<int> intList1 = this.ReadFlags();
      for (int index = 0; index < intList1.Count; ++index)
      {
        int flag = intList1[index];
        int bits = 0;
        if (index == 0)
        {
          if ((flag & 1) != 0)
            typedObject.Add("body", this.Decode());
          if ((flag & 2) != 0)
            typedObject.Add("clientId", this.Decode());
          if ((flag & 4) != 0)
            typedObject.Add("destination", this.Decode());
          if ((flag & 8) != 0)
            typedObject.Add("headers", this.Decode());
          if ((flag & 16) != 0)
            typedObject.Add("messageId", this.Decode());
          if ((flag & 32) != 0)
            typedObject.Add("timeStamp", this.Decode());
          if ((flag & 64) != 0)
            typedObject.Add("timeToLive", this.Decode());
          bits = 7;
        }
        else if (index == 1)
        {
          if ((flag & 1) != 0)
          {
            int num = (int) this.ReadByte();
            byte[] data = this.ReadByteArray();
            typedObject.Add("clientIdBytes", (object) data);
            typedObject.Add("clientId", (object) this.ByteArrayToID(data));
          }
          if ((flag & 2) != 0)
          {
            int num = (int) this.ReadByte();
            byte[] data = this.ReadByteArray();
            typedObject.Add("messageIdBytes", (object) data);
            typedObject.Add("messageId", (object) this.ByteArrayToID(data));
          }
          bits = 2;
        }
        this.ReadRemaining(flag, bits);
      }
      List<int> intList2 = this.ReadFlags();
      for (int index = 0; index < intList2.Count; ++index)
      {
        int flag = intList2[index];
        int bits = 0;
        if (index == 0)
        {
          if ((flag & 1) != 0)
            typedObject.Add("correlationId", this.Decode());
          if ((flag & 2) != 0)
          {
            int num = (int) this.ReadByte();
            byte[] data = this.ReadByteArray();
            typedObject.Add("correlationIdBytes", (object) data);
            typedObject.Add("correlationId", (object) this.ByteArrayToID(data));
          }
          bits = 2;
        }
        this.ReadRemaining(flag, bits);
      }
      return typedObject;
    }

    private TypedObject ReadDSK()
    {
      TypedObject typedObject = this.ReadDSA();
      typedObject.type = "DSK";
      List<int> intList = this.ReadFlags();
      for (int index = 0; index < intList.Count; ++index)
        this.ReadRemaining(intList[index], 0);
      return typedObject;
    }

    private List<int> ReadFlags()
    {
      List<int> intList = new List<int>();
      int num;
      do
      {
        num = this.ReadByteAsInt();
        intList.Add(num);
      }
      while ((num & 128) != 0);
      return intList;
    }

    private void ReadRemaining(int flag, int bits)
    {
      if (flag >> bits == 0)
        return;
      for (int index = bits; index < 6; ++index)
      {
        if ((flag >> index & 1) != 0)
          this.Decode();
      }
    }

    private string ByteArrayToID(byte[] data)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < data.Length; ++index)
      {
        if (index == 4 || index == 6 || (index == 8 || index == 10))
          stringBuilder.Append('-');
        stringBuilder.AppendFormat("{0:X2}", (object) data[index]);
      }
      return stringBuilder.ToString();
    }

    private object DecodeAMF0()
    {
      int num = (int) this.ReadByte();
      switch (num)
      {
        case 0:
          return (object) this.ReadIntAMF0();
        case 2:
          return (object) this.ReadStringAMF0();
        case 3:
          return (object) this.ReadObjectAMF0();
        case 5:
          return (object) null;
        case 17:
          return this.Decode();
        default:
          throw new NotImplementedException("AMF0 type not supported: " + (object) num);
      }
    }

    private string ReadStringAMF0()
    {
      int length = (this.ReadByteAsInt() << 8) + this.ReadByteAsInt();
      if (length == 0)
        return "";
      byte[] bytes = this.ReadBytes(length);
      try
      {
        return new UTF8Encoding().GetString(bytes);
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing AMF0 string from " + (object) bytes + (object) '\n' + ex.Message);
      }
    }

    private int ReadIntAMF0()
    {
      return (int) this.ReadDouble();
    }

    private TypedObject ReadObjectAMF0()
    {
      TypedObject typedObject = new TypedObject("Body");
      string key;
      while (!(key = this.ReadStringAMF0()).Equals(""))
      {
        byte num = this.ReadByte();
        switch (num)
        {
          case 0:
            typedObject.Add(key, (object) this.ReadDouble());
            continue;
          case 2:
            typedObject.Add(key, (object) this.ReadStringAMF0());
            continue;
          case 5:
            typedObject.Add(key, (object) null);
            continue;
          default:
            throw new NotImplementedException("AMF0 type not supported: " + (object) num);
        }
      }
      int num1 = (int) this.ReadByte();
      return typedObject;
    }
  }
}
