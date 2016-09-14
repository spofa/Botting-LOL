// Decompiled with JetBrains decompiler
// Type: PvPNetClient.Packet
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using System.Collections.Generic;

namespace PvPNetClient
{
  public class Packet
  {
    private byte[] dataBuffer;
    private int dataPos;
    private int dataSize;
    private int packetType;
    private List<byte> rawPacketBytes;

    public Packet()
    {
      this.rawPacketBytes = new List<byte>();
    }

    public void SetSize(int size)
    {
      this.dataSize = size;
      this.dataBuffer = new byte[this.dataSize];
    }

    public void SetType(int type)
    {
      this.packetType = type;
    }

    public void Add(byte b)
    {
      this.dataBuffer[this.dataPos++] = b;
    }

    public bool IsComplete()
    {
      return this.dataPos == this.dataSize;
    }

    public int GetSize()
    {
      return this.dataSize;
    }

    public int GetPacketType()
    {
      return this.packetType;
    }

    public byte[] GetData()
    {
      return this.dataBuffer;
    }

    public void AddToRaw(byte b)
    {
      this.rawPacketBytes.Add(b);
    }

    public void AddToRaw(byte[] b)
    {
      this.rawPacketBytes.AddRange((IEnumerable<byte>) b);
    }

    public byte[] GetRawData()
    {
      return this.rawPacketBytes.ToArray();
    }
  }
}
