// Decompiled with JetBrains decompiler
// Type: PvPNetClient.PvPNetConnection
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using ezBot;
using PvPNetClient.RiotObjects;
using PvPNetClient.RiotObjects.Leagues.Pojo;
using PvPNetClient.RiotObjects.Platform.Catalog.Champion;
using PvPNetClient.RiotObjects.Platform.Clientfacade.Domain;
using PvPNetClient.RiotObjects.Platform.Game;
using PvPNetClient.RiotObjects.Platform.Game.Message;
using PvPNetClient.RiotObjects.Platform.Game.Practice;
using PvPNetClient.RiotObjects.Platform.Harassment;
using PvPNetClient.RiotObjects.Platform.Leagues.Client.Dto;
using PvPNetClient.RiotObjects.Platform.Login;
using PvPNetClient.RiotObjects.Platform.Matchmaking;
using PvPNetClient.RiotObjects.Platform.Messaging;
using PvPNetClient.RiotObjects.Platform.Reroll.Pojo;
using PvPNetClient.RiotObjects.Platform.Statistics;
using PvPNetClient.RiotObjects.Platform.Statistics.Team;
using PvPNetClient.RiotObjects.Platform.Summoner;
using PvPNetClient.RiotObjects.Platform.Summoner.Boost;
using PvPNetClient.RiotObjects.Platform.Summoner.Masterybook;
using PvPNetClient.RiotObjects.Platform.Summoner.Runes;
using PvPNetClient.RiotObjects.Platform.Summoner.Spellbook;
using PvPNetClient.RiotObjects.Team;
using PvPNetClient.RiotObjects.Team.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PvPNetClient
{
    public class PvPNetConnection
    {
        private string garenaToken = "";
        private string userID = "";
        private Random rand = new Random();
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private int invokeID = 2;
        private List<int> pendingInvokes = new List<int>();
        private Dictionary<int, TypedObject> results = new Dictionary<int, TypedObject>();
        private Dictionary<int, RiotGamesObject> callbacks = new Dictionary<int, RiotGamesObject>();
        private int heartbeatCount = 1;
        private object isInvokingLock = new object();
        private bool isConnected;
        private bool isLoggedIn;
        private TcpClient client;
        private SslStream sslStream;
        private string ipAddress;
        private string authToken;
        private int accountID;
        private string sessionToken;
        private string DSId;
        private string user;
        private string password;
        private string server;
        private string loginQueue;
        private string locale;
        private string clientVersion;
        private bool useGarena;
        public Thread decodeThread;
        public Thread heartbeatThread;

        public event PvPNetConnection.OnConnectHandler OnConnect;

        public event PvPNetConnection.OnLoginQueueUpdateHandler OnLoginQueueUpdate;

        public event PvPNetConnection.OnLoginHandler OnLogin;

        public event PvPNetConnection.OnDisconnectHandler OnDisconnect;

        public event PvPNetConnection.OnMessageReceivedHandler OnMessageReceived;

        public event PvPNetConnection.OnErrorHandler OnError;

        public void Connect(string user, string password, Region region, string clientVersion)
        {
            if (this.isConnected)
                return;
            new Thread((ThreadStart)(() =>
           {
               this.user = user;
               this.password = password;
               this.clientVersion = clientVersion;
               this.server = RegionInfo.GetServerValue((System.Enum)region);
               this.loginQueue = RegionInfo.GetLoginQueueValue((System.Enum)region);
               this.locale = RegionInfo.GetLocaleValue((System.Enum)region);
               this.useGarena = RegionInfo.GetUseGarenaValue((System.Enum)region);
               this.garenaToken = password;
               try
               {
                   this.client = new TcpClient(this.server, 2099);
               }
               catch
               {
                   this.Error("Riots servers are currently unavailable.", ErrorType.AuthKey);
                   this.Disconnect();
                   return;
               }
               if (!this.GetAuthKey() || !this.GetIpAddress())
                   return;
               this.sslStream = new SslStream((Stream)this.client.GetStream(), false, new RemoteCertificateValidationCallback(this.AcceptAllCertificates));
               IAsyncResult asyncResult = this.sslStream.BeginAuthenticateAsClient(this.server, (AsyncCallback)null, (object)null);
               using (asyncResult.AsyncWaitHandle)
               {
                   if (asyncResult.AsyncWaitHandle.WaitOne(-1))
                       this.sslStream.EndAuthenticateAsClient(asyncResult);
               }
               if (!this.Handshake())
                   return;
               this.BeginReceive();
               if (!this.SendConnect() || !this.Login())
                   return;
               this.StartHeartbeat();
           })).Start();
        }

        private bool AcceptAllCertificates(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string reToken(string s)
        {
            return s.Replace("/", "%2F").Replace("+", "%2B").Replace("=", "%3D");
        }

        private bool GetAuthKey()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                string s = "payload=" + ("user=" + this.user + ",password=" + this.password);
                if (this.useGarena)
                    s = "payload=8393%20" + this.reToken(this.garenaToken);
                HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create(this.loginQueue + "login-queue/rest/queue/authenticate");
                httpWebRequest1.Proxy = (IWebProxy)null;
                httpWebRequest1.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest1.Method = "POST";
                Stream requestStream = httpWebRequest1.GetRequestStream();
                requestStream.Write(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetByteCount(s));
                Stream responseStream1 = httpWebRequest1.GetResponse().GetResponseStream();
                int num1;
                while ((num1 = responseStream1.ReadByte()) != -1)
                    stringBuilder.Append((char)num1);
                TypedObject typedObject = this.serializer.Deserialize<TypedObject>(stringBuilder.ToString());
                requestStream.Close();
                responseStream1.Close();
                httpWebRequest1.Abort();
                if (!typedObject.ContainsKey("token"))
                {
                    int num2 = typedObject.GetInt("node").Value;
                    string str = typedObject.GetString("champ");
                    int num3 = typedObject.GetInt("rate").Value;
                    int millisecondsTimeout = typedObject.GetInt("delay").Value;
                    int num4 = 0;
                    int num5 = 0;
                    foreach (Dictionary<string, object> dictionary in typedObject.GetArray("tickers"))
                    {
                        if ((int)dictionary["node"] == num2)
                        {
                            num4 = (int)dictionary["id"];
                            num5 = (int)dictionary["current"];
                            break;
                        }
                    }
                    while (num4 - num5 > num3)
                    {
                        stringBuilder.Clear();
                        if (this.OnLoginQueueUpdate != null)
                            this.OnLoginQueueUpdate((object)this, num4 - num5);
                        Thread.Sleep(millisecondsTimeout);
                        HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(this.loginQueue + "login-queue/rest/queue/ticker/" + str);
                        httpWebRequest2.Method = "GET";
                        Stream responseStream2 = httpWebRequest2.GetResponse().GetResponseStream();
                        int num6;
                        while ((num6 = responseStream2.ReadByte()) != -1)
                            stringBuilder.Append((char)num6);
                        typedObject = this.serializer.Deserialize<TypedObject>(stringBuilder.ToString());
                        responseStream2.Close();
                        httpWebRequest2.Abort();
                        if (typedObject != null)
                            num5 = this.HexToInt(typedObject.GetString(num2.ToString()));
                    }
                    while (true)
                    {
                        if (stringBuilder.ToString() != null)
                            goto label_30;
                        label_19:
                        try
                        {
                            stringBuilder.Clear();
                            if (num4 - num5 < 0)
                            {
                                if (this.OnLoginQueueUpdate != null)
                                    this.OnLoginQueueUpdate((object)this, 0);
                                else if (this.OnLoginQueueUpdate != null)
                                    this.OnLoginQueueUpdate((object)this, num4 - num5);
                            }
                            Thread.Sleep(millisecondsTimeout / 10);
                            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(this.loginQueue + "login-queue/rest/queue/authToken/" + this.user.ToLower());
                            httpWebRequest2.Method = "GET";
                            Stream responseStream2 = httpWebRequest2.GetResponse().GetResponseStream();
                            int num6;
                            while ((num6 = responseStream2.ReadByte()) != -1)
                                stringBuilder.Append((char)num6);
                            typedObject = this.serializer.Deserialize<TypedObject>(stringBuilder.ToString());
                            responseStream2.Close();
                            httpWebRequest2.Abort();
                            continue;
                        }
                        catch
                        {
                            continue;
                        }
                    label_30:
                        if (!typedObject.ContainsKey("token"))
                            goto label_19;
                        else
                            break;
                    }
                }
                if (this.OnLoginQueueUpdate != null)
                    this.OnLoginQueueUpdate((object)this, 0);
                this.authToken = typedObject.GetString("token");
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote name could not be resolved: '" + this.loginQueue + "'")
                {
                    this.Error("Please make sure you are connected the internet!", ErrorType.AuthKey);
                    this.Disconnect();
                }
                else if (ex.Message == "The remote server returned an error: (403) Forbidden.")
                {
                    this.Error("Your username or password is incorrect!", ErrorType.Password);
                    this.Disconnect();
                }
                else
                {
                    this.Error("Unable to get Auth Key \n" + (object)ex, ErrorType.AuthKey);
                    Console.WriteLine("FUCKER");
                    this.Disconnect();
                    Application.Exit();
                }
                return false;
            }
        }

        private int HexToInt(string hex)
        {
            int num = 0;
            for (int index = 0; index < hex.Length; ++index)
            {
                char ch = hex.ToCharArray()[index];
                num = (int)ch < 48 || (int)ch > 57 ? num * 16 + (int)ch - 97 + 10 : num * 16 + (int)ch - 48;
            }
            return num;
        }

        private bool GetIpAddress()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                CookieContainer cookieContainer = new CookieContainer();
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://ll.leagueoflegends.com/services/connection_info");
                httpWebRequest.CookieContainer = cookieContainer;
                WebResponse response = httpWebRequest.GetResponse();
                int num;
                while ((num = response.GetResponseStream().ReadByte()) != -1)
                    stringBuilder.Append((char)num);
                httpWebRequest.Abort();
                this.ipAddress = this.serializer.Deserialize<TypedObject>(stringBuilder.ToString()).GetString("ip_address");
                return true;
            }
            catch (Exception ex)
            {
                this.Error("Unable to connect to Riot Games web server \n" + ex.Message, ErrorType.General);
                this.Disconnect();
                return false;
            }
        }

        public static int EpochTime()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private bool Handshake()
        {
            this.sslStream.WriteByte((byte)3);
            int num1 = PvPNetConnection.EpochTime();
            byte[] buffer1 = new byte[1528];
            this.rand.NextBytes(buffer1);
            this.sslStream.Write(BitConverter.GetBytes(num1));
            this.sslStream.Write(BitConverter.GetBytes(0));
            this.sslStream.Write(buffer1);
            this.sslStream.Flush();
            byte num2 = (byte)this.sslStream.ReadByte();
            if ((int)num2 != 3)
            {
                Tools.ConsoleMessage("Server returned incorrect version in handshake: " + (object)num2, ConsoleColor.Red);
                this.Disconnect();
                return false;
            }
            byte[] buffer2 = new byte[1536];
            for (int index = 0; index < 1536; ++index)
                buffer2[index] = (byte)this.sslStream.ReadByte();
            int num3 = PvPNetConnection.EpochTime();
            this.sslStream.Write(buffer2, 0, 4);
            this.sslStream.Write(BitConverter.GetBytes(num3));
            this.sslStream.Write(buffer2, 8, 1528);
            this.sslStream.Flush();
            byte[] numArray = new byte[1536];
            for (int index = 0; index < 1536; ++index)
                numArray[index] = (byte)this.sslStream.ReadByte();
            bool flag = true;
            for (int index = 8; index < 1536; ++index)
            {
                if ((int)buffer1[index - 8] != (int)numArray[index])
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
                return true;
            Tools.ConsoleMessage("Server returned invalid handshake!", ConsoleColor.Red);
            this.Disconnect();
            return false;
        }

        private bool SendConnect()
        {
            byte[] buffer = new RTMPSEncoder().EncodeConnect(new Dictionary<string, object>()
      {
        {
          "app",
          (object) ""
        },
        {
          "flashVer",
          (object) "WIN 10,6,602,161"
        },
        {
          "swfUrl",
          (object) "app:/LolClient.swf/[[DYNAMIC]]/32"
        },
        {
          "tcUrl",
          (object) ("rtmps://" + this.server + ":" + (object) 2099)
        },
        {
          "fpad",
          (object) false
        },
        {
          "capabilities",
          (object) 239
        },
        {
          "audioCodecs",
          (object) 3575
        },
        {
          "videoCodecs",
          (object) 252
        },
        {
          "videoFunction",
          (object) 1
        },
        {
          "pageUrl",
          (object) null
        },
        {
          "objectEncoding",
          (object) 3
        }
      });
            this.sslStream.Write(buffer, 0, buffer.Length);
            while (!this.results.ContainsKey(1))
                Thread.Sleep(10);
            TypedObject result = this.results[1];
            this.results.Remove(1);
            if (result["result"].Equals((object)"_error"))
            {
                this.Error(this.GetErrorMessage(result), ErrorType.Connect);
                this.Disconnect();
                return false;
            }
            this.DSId = result.GetTO("data").GetString("id");
            this.isConnected = true;
            if (this.OnConnect != null)
                this.OnConnect((object)this, EventArgs.Empty);
            return true;
        }

        private bool Login()
        {
            AuthenticationCredentials authenticationCredentials = new AuthenticationCredentials();
            authenticationCredentials.Password = this.password;
            authenticationCredentials.ClientVersion = this.clientVersion;
            authenticationCredentials.IpAddress = this.ipAddress;
            authenticationCredentials.SecurityAnswer = (object)null;
            authenticationCredentials.Locale = this.locale;
            authenticationCredentials.Domain = "lolclient.lol.riotgames.com";
            authenticationCredentials.OldPassword = (object)null;
            authenticationCredentials.AuthToken = this.authToken;
            if (this.useGarena)
            {
                authenticationCredentials.PartnerCredentials = (object)("8393 " + this.garenaToken);
                authenticationCredentials.Username = this.userID;
                authenticationCredentials.Password = (string)null;
            }
            else
            {
                authenticationCredentials.PartnerCredentials = (object)null;
                authenticationCredentials.Username = this.user;
            }
            TypedObject result = this.GetResult(this.Invoke("loginService", (object)"login", (object)new object[1]
            {
        (object) authenticationCredentials.GetBaseTypedObject()
            }));
            if (result["result"].Equals((object)"_error"))
            {
                string str = (string)result.GetTO("data").GetTO("rootCause").GetArray("substitutionArguments")[1];
                this.Error(this.GetErrorMessage(result), ErrorType.Login);
                this.Disconnect();
                return false;
            }
            TypedObject to = result.GetTO("data").GetTO("body");
            this.sessionToken = to.GetString("token");
            this.accountID = to.GetTO("accountSummary").GetInt("accountId").Value;
            TypedObject packet = !this.useGarena ? this.WrapBody((object)Convert.ToBase64String(Encoding.UTF8.GetBytes(this.user.ToLower() + ":" + this.sessionToken)), "auth", (object)8) : this.WrapBody((object)Convert.ToBase64String(Encoding.UTF8.GetBytes(this.userID + ":" + this.sessionToken)), "auth", (object)8);
            packet.type = "flex.messaging.messages.CommandMessage";
            this.GetResult(this.Invoke(packet));
            this.isLoggedIn = true;
            if (this.OnLogin != null)
                this.OnLogin((object)this, this.user, this.ipAddress);
            return true;
        }

        private string GetErrorMessage(TypedObject message)
        {
            return message.GetTO("data").GetTO("rootCause").GetString("message");
        }

        private string GetErrorCode(TypedObject message)
        {
            return message.GetTO("data").GetTO("rootCause").GetString("errorCode");
        }

        private void StartHeartbeat()
        {
            this.heartbeatThread = new Thread((ThreadStart)(async () =>
           {
               while (true)
               {
                   try
                   {
                       long hbTime = (long)DateTime.Now.TimeOfDay.TotalMilliseconds;
                       string str = await this.PerformLCDSHeartBeat(this.accountID, this.sessionToken, this.heartbeatCount, DateTime.Now.ToString("ddd MMM d yyyy HH:mm:ss 'GMT-0700'"));
                       ++this.heartbeatCount;
                       while ((long)DateTime.Now.TimeOfDay.TotalMilliseconds - hbTime < 120000L)
                           Thread.Sleep(100);
                   }
                   catch
                   {
                   }
               }
           }));
            this.heartbeatThread.Start();
        }

        public void Disconnect()
        {
            new Thread((ThreadStart)(() =>
           {
               if (this.isConnected)
                   this.Join(this.Invoke("loginService", (object)"logout", (object)new object[1]
              {
            (object) this.authToken
               }));
               this.isConnected = false;
               if (this.heartbeatThread != null)
                   this.heartbeatThread.Abort();
               if (this.decodeThread != null)
                   this.decodeThread.Abort();
               this.invokeID = 2;
               this.heartbeatCount = 1;
               this.pendingInvokes.Clear();
               this.callbacks.Clear();
               this.results.Clear();
               this.client = (TcpClient)null;
               this.sslStream = (SslStream)null;
               if (this.OnDisconnect == null)
                   return;
               this.OnDisconnect((object)this, EventArgs.Empty);
           })).Start();
        }

        public static object y(string val)
        {
            return (object)Encoding.UTF8.GetString(Convert.FromBase64String(val));
        }

        private void Error(string message, string errorCode, ErrorType type)
        {
            Error error = new Error()
            {
                Type = type,
                Message = message,
                ErrorCode = errorCode
            };
            if (this.OnError == null)
                return;
            this.OnError((object)this, error);
        }

        private void Error(string message, ErrorType type)
        {
            this.Error(message, "", type);
        }

        private int Invoke(TypedObject packet)
        {
            lock (this.isInvokingLock)
            {
                int local_2 = this.NextInvokeID();
                this.pendingInvokes.Add(local_2);
                try
                {
                    byte[] local_3 = new RTMPSEncoder().EncodeInvoke(local_2, (object)packet);
                    this.sslStream.Write(local_3, 0, local_3.Length);
                    return local_2;
                }
                catch (IOException exception_0)
                {
                    this.pendingInvokes.Remove(local_2);
                    throw exception_0;
                }
            }
        }

        private int Invoke(string destination, object operation, object body)
        {
            return this.Invoke(this.WrapBody(body, destination, operation));
        }

        private int InvokeWithCallback(string destination, object operation, object body, RiotGamesObject cb)
        {
            if (this.isConnected)
            {
                this.callbacks.Add(this.invokeID, cb);
                return this.Invoke(destination, operation, body);
            }
            this.Error("The client is not connected. Please make sure to connect before tring to execute an Invoke command.", ErrorType.Invoke);
            this.Disconnect();
            return -1;
        }

        protected TypedObject WrapBody(object body, string destination, object operation)
        {
            TypedObject typedObject1 = new TypedObject();
            typedObject1.Add("DSRequestTimeout", (object)60);
            typedObject1.Add("DSId", (object)this.DSId);
            typedObject1.Add("DSEndpoint", (object)"my-rtmps");
            TypedObject typedObject2 = new TypedObject("flex.messaging.messages.RemotingMessage");
            typedObject2.Add("operation", operation);
            typedObject2.Add("source", (object)null);
            typedObject2.Add("timestamp", (object)0);
            typedObject2.Add("messageId", (object)RTMPSEncoder.RandomUID());
            typedObject2.Add("timeToLive", (object)0);
            typedObject2.Add("clientId", (object)null);
            typedObject2.Add("destination", (object)destination);
            typedObject2.Add("body", body);
            typedObject2.Add("headers", (object)typedObject1);
            return typedObject2;
        }

        protected int NextInvokeID()
        {
            return this.invokeID++;
        }

        private void MessageReceived(object messageBody)
        {
            if (this.OnMessageReceived == null)
                return;
            this.OnMessageReceived((object)this, messageBody);
        }

        private void BeginReceive()
        {
            this.decodeThread = new Thread((ThreadStart)(() =>
           {
               int? nullable1 = null;
               try
               {
                   Dictionary<int, Packet> dictionary1 = new Dictionary<int, Packet>();
                   Dictionary<int, Packet> dictionary2 = new Dictionary<int, Packet>();

                   while (true)
                   {
                       TypedObject message = null;

                       do
                       {
                           int key;
                           Packet packet;
                           do
                           {
                               byte num1 = (byte)this.sslStream.ReadByte();
                               List<byte> byteList = new List<byte>();
                               if ((int)num1 == (int)byte.MaxValue)
                                   this.Disconnect();
                               key = 0;
                               if (((int)num1 & 3) != 0)
                               {
                                   key = (int)num1 & 63;
                                   byteList.Add(num1);
                               }
                               else if (((int)num1 & 1) != 0)
                               {
                                   byte num2 = (byte)this.sslStream.ReadByte();
                                   key = 64 + (int)num2;
                                   byteList.Add(num1);
                                   byteList.Add(num2);
                               }
                               else if (((int)num1 & 2) != 0)
                               {
                                   byte num2 = (byte)this.sslStream.ReadByte();
                                   byte num3 = (byte)this.sslStream.ReadByte();
                                   byteList.Add(num1);
                                   byteList.Add(num2);
                                   byteList.Add(num3);
                                   key = 64 + (int)num2 + 256 * (int)num3;
                               }
                               int num4 = (int)num1 & 192;
                               int num5 = 0;
                               if (num4 == 0)
                                   num5 = 12;
                               else if (num4 == 64)
                                   num5 = 8;
                               else if (num4 == 128)
                                   num5 = 4;
                               else if (num4 == 192)
                                   num5 = 0;
                               if (!dictionary2.ContainsKey(key))
                                   dictionary2.Add(key, new Packet());
                               packet = dictionary2[key];
                               packet.AddToRaw(byteList.ToArray());
                               if (num5 == 12)
                               {
                                   byte[] numArray1 = new byte[3];
                                   for (int index = 0; index < 3; ++index)
                                   {
                                       numArray1[index] = (byte)this.sslStream.ReadByte();
                                       packet.AddToRaw(numArray1[index]);
                                   }
                                   byte[] numArray2 = new byte[3];
                                   for (int index = 0; index < 3; ++index)
                                   {
                                       numArray2[index] = (byte)this.sslStream.ReadByte();
                                       packet.AddToRaw(numArray2[index]);
                                   }
                                   int size = 0;
                                   for (int index = 0; index < 3; ++index)
                                       size = size * 256 + ((int)numArray2[index] & (int)byte.MaxValue);
                                   packet.SetSize(size);
                                   int type = this.sslStream.ReadByte();
                                   packet.AddToRaw((byte)type);
                                   packet.SetType(type);
                                   byte[] numArray3 = new byte[4];
                                   for (int index = 0; index < 4; ++index)
                                   {
                                       numArray3[index] = (byte)this.sslStream.ReadByte();
                                       packet.AddToRaw(numArray3[index]);
                                   }
                               }
                               else if (num5 == 8)
                               {
                                   byte[] numArray1 = new byte[3];
                                   for (int index = 0; index < 3; ++index)
                                   {
                                       numArray1[index] = (byte)this.sslStream.ReadByte();
                                       packet.AddToRaw(numArray1[index]);
                                   }
                                   byte[] numArray2 = new byte[3];
                                   for (int index = 0; index < 3; ++index)
                                   {
                                       numArray2[index] = (byte)this.sslStream.ReadByte();
                                       packet.AddToRaw(numArray2[index]);
                                   }
                                   int size = 0;
                                   for (int index = 0; index < 3; ++index)
                                       size = size * 256 + ((int)numArray2[index] & (int)byte.MaxValue);
                                   packet.SetSize(size);
                                   int type = this.sslStream.ReadByte();
                                   packet.AddToRaw((byte)type);
                                   packet.SetType(type);
                               }
                               else if (num5 == 4)
                               {
                                   byte[] numArray = new byte[3];
                                   for (int index = 0; index < 3; ++index)
                                   {
                                       numArray[index] = (byte)this.sslStream.ReadByte();
                                       packet.AddToRaw(numArray[index]);
                                   }
                                   if (packet.GetSize() == 0 && packet.GetPacketType() == 0 && dictionary1.ContainsKey(key))
                                   {
                                       packet.SetSize(dictionary1[key].GetSize());
                                       packet.SetType(dictionary1[key].GetPacketType());
                                   }
                               }
                               else if (num5 == 0 && packet.GetSize() == 0 && (packet.GetPacketType() == 0 && dictionary1.ContainsKey(key)))
                               {
                                   packet.SetSize(dictionary1[key].GetSize());
                                   packet.SetType(dictionary1[key].GetPacketType());
                               }
                               for (int index = 0; index < 128; ++index)
                               {
                                   byte b = (byte)this.sslStream.ReadByte();
                                   packet.Add(b);
                                   packet.AddToRaw(b);
                                   if (packet.IsComplete())
                                       break;
                               }
                           }
                           while (!packet.IsComplete());
                           if (dictionary1.ContainsKey(key))
                               dictionary1.Remove(key);
                           dictionary1.Add(key, packet);
                           if (dictionary2.ContainsKey(key))
                               dictionary2.Remove(key);
                           RTMPSDecoder rtmpsDecoder = new RTMPSDecoder();
                           if (packet.GetPacketType() == 20)
                               message = rtmpsDecoder.DecodeConnect(packet.GetData());
                           else if (packet.GetPacketType() == 17)
                           {
                               message = rtmpsDecoder.DecodeInvoke(packet.GetData());
                           }
                           else
                           {
                               if (packet.GetPacketType() == 6)
                               {
                                   byte[] data = packet.GetData();
                                   int num1 = 0;
                                   for (int index = 0; index < 4; ++index)
                                       num1 = num1 * 256 + ((int)data[index] & (int)byte.MaxValue);
                                   int num2 = (int)data[4];
                                   continue;
                               }
                               if (packet.GetPacketType() == 5)
                               {
                                   byte[] data = packet.GetData();
                                   int num = 0;
                                   for (int index = 0; index < 4; ++index)
                                       num = num * 256 + ((int)data[index] & (int)byte.MaxValue);
                                   continue;
                               }
                               if (packet.GetPacketType() == 3)
                               {
                                   byte[] data = packet.GetData();
                                   int num = 0;
                                   for (int index = 0; index < 4; ++index)
                                       num = num * 256 + ((int)data[index] & (int)byte.MaxValue);
                                   continue;
                               }
                               if (packet.GetPacketType() == 2)
                               {
                                   packet.GetData();
                                   continue;
                               }
                               if (packet.GetPacketType() == 1)
                               {
                                   packet.GetData();
                                   continue;
                               }
                               continue;
                           }
                           nullable1 = message.GetInt("invokeId");
                           if (message["result"].Equals((object)"_error"))
                               this.Error(this.GetErrorMessage(message), this.GetErrorCode(message), ErrorType.Receive);
                           if (message["result"].Equals((object)"receive") && message.GetTO("data") != null)
                           {
                               TypedObject to = message.GetTO("data");
                               if (to.ContainsKey("body") && to["body"] is TypedObject)
                                   new Thread((ThreadStart)(() =>
                             {
                                 TypedObject result = (TypedObject)to["body"];
                                 if (result.type.Equals("com.riotgames.platform.game.GameDTO"))
                                     this.MessageReceived((object)new GameDTO(result));
                                 else if (result.type.Equals("com.riotgames.platform.game.PlayerCredentialsDto"))
                                     this.MessageReceived((object)new PlayerCredentialsDto(result));
                                 else if (result.type.Equals("com.riotgames.platform.game.message.GameNotification"))
                                     this.MessageReceived((object)new GameNotification(result));
                                 else if (result.type.Equals("com.riotgames.platform.matchmaking.SearchingForMatchNotification"))
                                     this.MessageReceived((object)new SearchingForMatchNotification(result));
                                 else if (result.type.Equals("com.riotgames.platform.messaging.StoreFulfillmentNotification"))
                                     this.MessageReceived((object)new StoreFulfillmentNotification(result));
                                 else if (result.type.Equals("com.riotgames.platform.messaging.StoreFulfillmentNotification"))
                                     this.MessageReceived((object)new StoreAccountBalanceNotification(result));
                                 else
                                     this.MessageReceived((object)result);
                             })).Start();
                           }
                       }
                       while (!nullable1.HasValue);
                       int? nullable2 = nullable1;
                       if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) == 0)
                       {
                           if (this.callbacks.ContainsKey(nullable1.Value))
                           {
                               RiotGamesObject cb = this.callbacks[nullable1.Value];
                               this.callbacks.Remove(nullable1.Value);
                               if (cb != null)
                               {
                                   TypedObject messageBody = message.GetTO("data").GetTO("body");
                                   new Thread((ThreadStart)(() => cb.DoCallback(messageBody))).Start();
                               }
                           }
                           else
                               this.results.Add(nullable1.Value, message);
                       }
                       this.pendingInvokes.Remove(nullable1.Value);
                   }
               }
               catch (Exception ex)
               {
                   if (!this.IsConnected())
                       return;
                   this.Error(ex.Message, ErrorType.Receive);
               }
           }));
            this.decodeThread.Start();
        }

        private TypedObject GetResult(int id)
        {
            while (this.IsConnected() && !this.results.ContainsKey(id))
                Thread.Sleep(10);
            if (!this.IsConnected())
                return (TypedObject)null;
            TypedObject result = this.results[id];
            this.results.Remove(id);
            return result;
        }

        private TypedObject PeekResult(int id)
        {
            if (!this.results.ContainsKey(id))
                return (TypedObject)null;
            TypedObject result = this.results[id];
            this.results.Remove(id);
            return result;
        }

        private void Join()
        {
            while (this.pendingInvokes.Count > 0)
                Thread.Sleep(10);
        }

        private void Join(int id)
        {
            while (this.IsConnected() && this.pendingInvokes.Contains(id))
                Thread.Sleep(10);
        }

        private void Cancel(int id)
        {
            this.pendingInvokes.Remove(id);
            if (this.PeekResult(id) != null)
                return;
            this.callbacks.Add(id, (RiotGamesObject)null);
            if (this.PeekResult(id) == null)
                return;
            this.callbacks.Remove(id);
        }

        public bool IsConnected()
        {
            return this.isConnected;
        }

        public bool IsLoggedIn()
        {
            return this.isLoggedIn;
        }

        public double AccountID()
        {
            return (double)this.accountID;
        }

        private void Login(AuthenticationCredentials arg0, Session.Callback callback)
        {
            Session session = new Session(callback);
            this.InvokeWithCallback("loginService", (object)"login", (object)new object[1]
            {
        (object) arg0.GetBaseTypedObject()
            }, (RiotGamesObject)session);
        }

        private async Task<Session> Login(AuthenticationCredentials arg0)
        {
            int Id = this.Invoke("loginService", (object)"login", (object)new object[1]
            {
        (object) arg0.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            Session result = new Session(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> Subscribe(string service, double accountId)
        {
            TypedObject body = this.WrapBody((object)new TypedObject(), "messagingDestination", (object)0);
            body.type = "flex.messaging.messages.CommandMessage";
            TypedObject headers = body.GetTO("headers");
            if (service == "bc")
                headers.Add("DSSubtopic", (object)"bc");
            else
                headers.Add("DSSubtopic", (object)(service + "-" + (object)this.accountID));
            headers.Remove("DSRequestTimeout");
            body["clientId"] = (object)(service + "-" + (object)this.accountID);
            int Id = this.Invoke(body);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.GetResult(Id);
            return (object)null;
        }

        public void GetLoginDataPacketForUser(LoginDataPacket.Callback callback)
        {
            this.InvokeWithCallback("clientFacadeService", (object)"getLoginDataPacketForUser", (object)new object[0], (RiotGamesObject)new LoginDataPacket(callback));
        }

        public async Task<LoginDataPacket> GetLoginDataPacketForUser()
        {
            int Id = this.Invoke("clientFacadeService", (object)"getLoginDataPacketForUser", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            LoginDataPacket result = new LoginDataPacket(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<GameQueueConfig[]> GetAvailableQueues()
        {
            int Id = this.Invoke("matchmakerService", (object)"getAvailableQueues", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            GameQueueConfig[] result = new GameQueueConfig[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = new GameQueueConfig((TypedObject)this.results[Id].GetTO("data").GetArray("body")[index]);
            this.results.Remove(Id);
            return result;
        }

        public void GetSumonerActiveBoosts(SummonerActiveBoostsDTO.Callback callback)
        {
            this.InvokeWithCallback("inventoryService", (object)"getSumonerActiveBoosts", (object)new object[0], (RiotGamesObject)new SummonerActiveBoostsDTO(callback));
        }

        public async Task<SummonerActiveBoostsDTO> GetSumonerActiveBoosts()
        {
            int Id = this.Invoke("inventoryService", (object)"getSumonerActiveBoosts", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SummonerActiveBoostsDTO result = new SummonerActiveBoostsDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<ChampionDTO[]> GetAvailableChampions()
        {
            int Id = this.Invoke("inventoryService", (object)"getAvailableChampions", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            ChampionDTO[] result = new ChampionDTO[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = new ChampionDTO((TypedObject)this.results[Id].GetTO("data").GetArray("body")[index]);
            this.results.Remove(Id);
            return result;
        }

        public void GetSummonerRuneInventory(double summonerId, SummonerRuneInventory.Callback callback)
        {
            SummonerRuneInventory summonerRuneInventory = new SummonerRuneInventory(callback);
            this.InvokeWithCallback("summonerRuneService", (object)"getSummonerRuneInventory", (object)new object[1]
            {
        (object) summonerId
            }, (RiotGamesObject)summonerRuneInventory);
        }

        public async Task<SummonerRuneInventory> GetSummonerRuneInventory(double summonerId)
        {
            int Id = this.Invoke("summonerRuneService", (object)"getSummonerRuneInventory", (object)new object[1]
            {
        (object) summonerId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SummonerRuneInventory result = new SummonerRuneInventory(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<string> PerformLCDSHeartBeat(int arg0, string arg1, int arg2, string arg3)
        {
            int Id = this.Invoke("loginService", (object)"performLCDSHeartBeat", (object)new object[4]
            {
        (object) arg0,
        (object) arg1,
        (object) arg2,
        (object) arg3
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            string result = (string)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public void GetMyLeaguePositions(SummonerLeagueItemsDTO.Callback callback)
        {
            this.InvokeWithCallback("leaguesServiceProxy", (object)"getMyLeaguePositions", (object)new object[0], (RiotGamesObject)new SummonerLeagueItemsDTO(callback));
        }

        public async Task<SummonerLeagueItemsDTO> GetMyLeaguePositions()
        {
            int Id = this.Invoke("leaguesServiceProxy", (object)"getMyLeaguePositions", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SummonerLeagueItemsDTO result = new SummonerLeagueItemsDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> LoadPreferencesByKey(string arg0, double arg1, bool arg2)
        {
            int Id = this.Invoke("playerPreferencesService", (object)"loadPreferencesByKey", (object)new object[3]
            {
        (object) arg0,
        (object) arg1,
        (object) arg2
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public void GetMasteryBook(double summonerId, MasteryBookDTO.Callback callback)
        {
            MasteryBookDTO masteryBookDto = new MasteryBookDTO(callback);
            this.InvokeWithCallback("masteryBookService", (object)"getMasteryBook", (object)new object[1]
            {
        (object) summonerId
            }, (RiotGamesObject)masteryBookDto);
        }

        public async Task<MasteryBookDTO> GetMasteryBook(double summonerId)
        {
            int Id = this.Invoke("masteryBookService", (object)"getMasteryBook", (object)new object[1]
            {
        (object) summonerId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            MasteryBookDTO result = new MasteryBookDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void CreatePlayer(PlayerDTO.Callback callback)
        {
            this.InvokeWithCallback("summonerTeamService", (object)"createPlayer", (object)new object[0], (RiotGamesObject)new PlayerDTO(callback));
        }

        public async Task<PlayerDTO> CreatePlayer()
        {
            int Id = this.Invoke("summonerTeamService", (object)"createPlayer", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            PlayerDTO result = new PlayerDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<AllSummonerData> CreateDefaultSummoner(string summonerName)
        {
            int Id = this.Invoke("summonerService", (object)"createDefaultSummoner", (object)new object[1]
            {
        (object) summonerName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            AllSummonerData result = new AllSummonerData(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<string[]> GetSummonerNames(double[] summonerIds)
        {
            int Id = this.Invoke("summonerService", (object)"getSummonerNames", (object)new object[1]
            {
        (object) summonerIds.Cast<object>().ToArray<object>()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            string[] result = new string[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = (string)this.results[Id].GetTO("data").GetArray("body")[index];
            this.results.Remove(Id);
            return result;
        }

        public void GetChallengerLeague(string queueType, LeagueListDTO.Callback callback)
        {
            LeagueListDTO leagueListDto = new LeagueListDTO(callback);
            this.InvokeWithCallback("leaguesServiceProxy", (object)"getChallengerLeague", (object)new object[1]
            {
        (object) queueType
            }, (RiotGamesObject)leagueListDto);
        }

        public async Task<LeagueListDTO> GetChallengerLeague(string queueType)
        {
            int Id = this.Invoke("leaguesServiceProxy", (object)"getChallengerLeague", (object)new object[1]
            {
        (object) queueType
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            LeagueListDTO result = new LeagueListDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetAllMyLeagues(SummonerLeaguesDTO.Callback callback)
        {
            this.InvokeWithCallback("leaguesServiceProxy", (object)"getAllMyLeagues", (object)new object[0], (RiotGamesObject)new SummonerLeaguesDTO(callback));
        }

        public async Task<SummonerLeaguesDTO> GetAllMyLeagues()
        {
            int Id = this.Invoke("leaguesServiceProxy", (object)"getAllMyLeagues", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SummonerLeaguesDTO result = new SummonerLeaguesDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetAllSummonerDataByAccount(double accountId, AllSummonerData.Callback callback)
        {
            AllSummonerData allSummonerData = new AllSummonerData(callback);
            this.InvokeWithCallback("summonerService", (object)"getAllSummonerDataByAccount", (object)new object[1]
            {
        (object) accountId
            }, (RiotGamesObject)allSummonerData);
        }

        public async Task<AllSummonerData> GetAllSummonerDataByAccount(double accountId)
        {
            int Id = this.Invoke("summonerService", (object)"getAllSummonerDataByAccount", (object)new object[1]
            {
        (object) accountId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            AllSummonerData result = new AllSummonerData(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetPointsBalance(PointSummary.Callback callback)
        {
            this.InvokeWithCallback("lcdsRerollService", (object)"getPointsBalance", (object)new object[0], (RiotGamesObject)new PointSummary(callback));
        }

        public async Task<PointSummary> GetPointsBalance()
        {
            int Id = this.Invoke("lcdsRerollService", (object)"getPointsBalance", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            PointSummary result = new PointSummary(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<string> GetSummonerIcons(double[] summonerIds)
        {
            int Id = this.Invoke("summonerService", (object)"getSummonerIcons", (object)new object[1]
            {
        (object) summonerIds.Cast<object>().ToArray<object>()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            string result = (string)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public void CallKudos(string arg0, LcdsResponseString.Callback callback)
        {
            LcdsResponseString lcdsResponseString = new LcdsResponseString(callback);
            this.InvokeWithCallback("clientFacadeService", (object)"callKudos", (object)new object[1] { (object)arg0 }, (RiotGamesObject)lcdsResponseString);
        }

        public async Task<LcdsResponseString> CallKudos(string arg0)
        {
            int Id = this.Invoke("clientFacadeService", (object)"callKudos", (object)new object[1] { (object)arg0 });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            LcdsResponseString result = new LcdsResponseString(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void RetrievePlayerStatsByAccountId(double accountId, string season, PlayerLifetimeStats.Callback callback)
        {
            PlayerLifetimeStats playerLifetimeStats = new PlayerLifetimeStats(callback);
            this.InvokeWithCallback("playerStatsService", (object)"retrievePlayerStatsByAccountId", (object)new object[2]
            {
        (object) accountId,
        (object) season
            }, (RiotGamesObject)playerLifetimeStats);
        }

        public async Task<PlayerLifetimeStats> RetrievePlayerStatsByAccountId(double accountId, string season)
        {
            int Id = this.Invoke("playerStatsService", (object)"retrievePlayerStatsByAccountId", (object)new object[2]
            {
        (object) accountId,
        (object) season
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            PlayerLifetimeStats result = new PlayerLifetimeStats(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<ChampionStatInfo[]> RetrieveTopPlayedChampions(double accountId, string gameMode)
        {
            int Id = this.Invoke("playerStatsService", (object)"retrieveTopPlayedChampions", (object)new object[2]
            {
        (object) accountId,
        (object) gameMode
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            ChampionStatInfo[] result = new ChampionStatInfo[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = new ChampionStatInfo((TypedObject)this.results[Id].GetTO("data").GetArray("body")[index]);
            this.results.Remove(Id);
            return result;
        }

        public void GetSummonerByName(string summonerName, PublicSummoner.Callback callback)
        {
            PublicSummoner publicSummoner = new PublicSummoner(callback);
            this.InvokeWithCallback("summonerService", (object)"getSummonerByName", (object)new object[1]
            {
        (object) summonerName
            }, (RiotGamesObject)publicSummoner);
        }

        public async Task<PublicSummoner> GetSummonerByName(string summonerName)
        {
            int Id = this.Invoke("summonerService", (object)"getSummonerByName", (object)new object[1]
            {
        (object) summonerName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            PublicSummoner result = new PublicSummoner(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetAggregatedStats(double summonerId, string gameMode, string season, AggregatedStats.Callback callback)
        {
            AggregatedStats aggregatedStats = new AggregatedStats(callback);
            this.InvokeWithCallback("playerStatsService", (object)"getAggregatedStats", (object)new object[3]
            {
        (object) summonerId,
        (object) gameMode,
        (object) season
            }, (RiotGamesObject)aggregatedStats);
        }

        public async Task<AggregatedStats> GetAggregatedStats(double summonerId, string gameMode, string season)
        {
            int Id = this.Invoke("playerStatsService", (object)"getAggregatedStats", (object)new object[3]
            {
        (object) summonerId,
        (object) gameMode,
        (object) season
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            AggregatedStats result = new AggregatedStats(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetRecentGames(double accountId, RecentGames.Callback callback)
        {
            RecentGames recentGames = new RecentGames(callback);
            this.InvokeWithCallback("playerStatsService", (object)"getRecentGames", (object)new object[1]
            {
        (object) accountId
            }, (RiotGamesObject)recentGames);
        }

        public async Task<RecentGames> GetRecentGames(double accountId)
        {
            int Id = this.Invoke("playerStatsService", (object)"getRecentGames", (object)new object[1]
            {
        (object) accountId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            RecentGames result = new RecentGames(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void FindTeamById(TeamId teamId, TeamDTO.Callback callback)
        {
            TeamDTO teamDto = new TeamDTO(callback);
            this.InvokeWithCallback("summonerTeamService", (object)"findTeamById", (object)new object[1]
            {
        (object) teamId.GetBaseTypedObject()
            }, (RiotGamesObject)teamDto);
        }

        public async Task<TeamDTO> FindTeamById(TeamId teamId)
        {
            int Id = this.Invoke("summonerTeamService", (object)"findTeamById", (object)new object[1]
            {
        (object) teamId.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            TeamDTO result = new TeamDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetLeaguesForTeam(string teamName, SummonerLeaguesDTO.Callback callback)
        {
            SummonerLeaguesDTO summonerLeaguesDto = new SummonerLeaguesDTO(callback);
            this.InvokeWithCallback("leaguesServiceProxy", (object)"getLeaguesForTeam", (object)new object[1]
            {
        (object) teamName
            }, (RiotGamesObject)summonerLeaguesDto);
        }

        public async Task<SummonerLeaguesDTO> GetLeaguesForTeam(string teamName)
        {
            int Id = this.Invoke("leaguesServiceProxy", (object)"getLeaguesForTeam", (object)new object[1]
            {
        (object) teamName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SummonerLeaguesDTO result = new SummonerLeaguesDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<TeamAggregatedStatsDTO[]> GetTeamAggregatedStats(TeamId arg0)
        {
            int Id = this.Invoke("playerStatsService", (object)"getTeamAggregatedStats", (object)new object[1]
            {
        (object) arg0.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TeamAggregatedStatsDTO[] result = new TeamAggregatedStatsDTO[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = new TeamAggregatedStatsDTO((TypedObject)this.results[Id].GetTO("data").GetArray("body")[index]);
            this.results.Remove(Id);
            return result;
        }

        public void GetTeamEndOfGameStats(TeamId arg0, double arg1, EndOfGameStats.Callback callback)
        {
            EndOfGameStats endOfGameStats = new EndOfGameStats(callback);
            this.InvokeWithCallback("playerStatsService", (object)"getTeamEndOfGameStats", (object)new object[2]
            {
        (object) arg0.GetBaseTypedObject(),
        (object) arg1
            }, (RiotGamesObject)endOfGameStats);
        }

        public async Task<EndOfGameStats> GetTeamEndOfGameStats(TeamId arg0, double arg1)
        {
            int Id = this.Invoke("playerStatsService", (object)"getTeamEndOfGameStats", (object)new object[2]
            {
        (object) arg0.GetBaseTypedObject(),
        (object) arg1
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            EndOfGameStats result = new EndOfGameStats(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> DisbandTeam(TeamId teamId)
        {
            int Id = this.Invoke("summonerTeamService", (object)"disbandTeam", (object)new object[1]
            {
        (object) teamId.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<bool> IsNameValidAndAvailable(string teamName)
        {
            int Id = this.Invoke("summonerTeamService", (object)"isNameValidAndAvailable", (object)new object[1]
            {
        (object) teamName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<bool> IsTagValidAndAvailable(string tagName)
        {
            int Id = this.Invoke("summonerTeamService", (object)"isTagValidAndAvailable", (object)new object[1]
            {
        (object) tagName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public void CreateTeam(string teamName, string tagName, TeamDTO.Callback callback)
        {
            TeamDTO teamDto = new TeamDTO(callback);
            this.InvokeWithCallback("summonerTeamService", (object)"createTeam", (object)new object[2]
            {
        (object) teamName,
        (object) tagName
            }, (RiotGamesObject)teamDto);
        }

        public async Task<TeamDTO> CreateTeam(string teamName, string tagName)
        {
            int Id = this.Invoke("summonerTeamService", (object)"createTeam", (object)new object[2]
            {
        (object) teamName,
        (object) tagName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            TeamDTO result = new TeamDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void InvitePlayer(double summonerId, TeamId teamId, TeamDTO.Callback callback)
        {
            TeamDTO teamDto = new TeamDTO(callback);
            this.InvokeWithCallback("summonerTeamService", (object)"invitePlayer", (object)new object[2]
            {
        (object) summonerId,
        (object) teamId.GetBaseTypedObject()
            }, (RiotGamesObject)teamDto);
        }

        public async Task<TeamDTO> InvitePlayer(double summonerId, TeamId teamId)
        {
            int Id = this.Invoke("summonerTeamService", (object)"invitePlayer", (object)new object[2]
            {
        (object) summonerId,
        (object) teamId.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            TeamDTO result = new TeamDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void KickPlayer(double summonerId, TeamId teamId, TeamDTO.Callback callback)
        {
            TeamDTO teamDto = new TeamDTO(callback);
            this.InvokeWithCallback("summonerTeamService", (object)"kickPlayer", (object)new object[2]
            {
        (object) summonerId,
        (object) teamId.GetBaseTypedObject()
            }, (RiotGamesObject)teamDto);
        }

        public async Task<TeamDTO> KickPlayer(double summonerId, TeamId teamId)
        {
            int Id = this.Invoke("summonerTeamService", (object)"kickPlayer", (object)new object[2]
            {
        (object) summonerId,
        (object) teamId.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            TeamDTO result = new TeamDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetAllLeaguesForPlayer(double summonerId, SummonerLeaguesDTO.Callback callback)
        {
            SummonerLeaguesDTO summonerLeaguesDto = new SummonerLeaguesDTO(callback);
            this.InvokeWithCallback("leaguesServiceProxy", (object)"getAllLeaguesForPlayer", (object)new object[1]
            {
        (object) summonerId
            }, (RiotGamesObject)summonerLeaguesDto);
        }

        public async Task<SummonerLeaguesDTO> GetAllLeaguesForPlayer(double summonerId)
        {
            int Id = this.Invoke("leaguesServiceProxy", (object)"getAllLeaguesForPlayer", (object)new object[1]
            {
        (object) summonerId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SummonerLeaguesDTO result = new SummonerLeaguesDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetAllPublicSummonerDataByAccount(double accountId, AllPublicSummonerDataDTO.Callback callback)
        {
            AllPublicSummonerDataDTO publicSummonerDataDto = new AllPublicSummonerDataDTO(callback);
            this.InvokeWithCallback("summonerService", (object)"getAllPublicSummonerDataByAccount", (object)new object[1]
            {
        (object) accountId
            }, (RiotGamesObject)publicSummonerDataDto);
        }

        public async Task<AllPublicSummonerDataDTO> GetAllPublicSummonerDataByAccount(double accountId)
        {
            int Id = this.Invoke("summonerService", (object)"getAllPublicSummonerDataByAccount", (object)new object[1]
            {
        (object) accountId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            AllPublicSummonerDataDTO result = new AllPublicSummonerDataDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void FindPlayer(double summonerId, PlayerDTO.Callback callback)
        {
            PlayerDTO playerDto = new PlayerDTO(callback);
            this.InvokeWithCallback("summonerTeamService", (object)"findPlayer", (object)new object[1]
            {
        (object) summonerId
            }, (RiotGamesObject)playerDto);
        }

        public async Task<PlayerDTO> FindPlayer(double summonerId)
        {
            int Id = this.Invoke("summonerTeamService", (object)"findPlayer", (object)new object[1]
            {
        (object) summonerId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            PlayerDTO result = new PlayerDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void GetSpellBook(double summonerId, SpellBookDTO.Callback callback)
        {
            SpellBookDTO spellBookDto = new SpellBookDTO(callback);
            this.InvokeWithCallback("spellBookService", (object)"getSpellBook", (object)new object[1]
            {
        (object) summonerId
            }, (RiotGamesObject)spellBookDto);
        }

        public async Task<SpellBookDTO> GetSpellBook(double summonerId)
        {
            int Id = this.Invoke("spellBookService", (object)"getSpellBook", (object)new object[1]
            {
        (object) summonerId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SpellBookDTO result = new SpellBookDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public void AttachToQueue(MatchMakerParams matchMakerParams, SearchingForMatchNotification.Callback callback)
        {
            SearchingForMatchNotification matchNotification = new SearchingForMatchNotification(callback);
            this.InvokeWithCallback("matchmakerService", (object)"attachToQueue", (object)new object[1]
            {
        (object) matchMakerParams.GetBaseTypedObject()
            }, (RiotGamesObject)matchNotification);
        }

        public async Task<SearchingForMatchNotification> AttachToQueue(MatchMakerParams matchMakerParams)
        {
            int Id = this.Invoke("matchmakerService", (object)"attachToQueue", (object)new object[1]
            {
        (object) matchMakerParams.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SearchingForMatchNotification result = new SearchingForMatchNotification(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<bool> CancelFromQueueIfPossible(int queueId)
        {
            int Id = this.Invoke("matchmakerService", (object)"cancelFromQueueIfPossible", (object)new object[1]
            {
        (object) queueId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<string> GetStoreUrl()
        {
            int Id = this.Invoke("loginService", (object)"getStoreUrl", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            string result = (string)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<PracticeGameSearchResult[]> ListAllPracticeGames()
        {
            int Id = this.Invoke("gameService", (object)"listAllPracticeGames", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            PracticeGameSearchResult[] result = new PracticeGameSearchResult[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = new PracticeGameSearchResult((TypedObject)this.results[Id].GetTO("data").GetArray("body")[index]);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> JoinGame(double gameId)
        {
            int Id = this.Invoke("gameService", (object)"joinGame", (object)new object[2]
            {
        (object) gameId,
        null
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> JoinGame(double gameId, string password)
        {
            int Id = this.Invoke("gameService", (object)"joinGame", (object)new object[2]
            {
        (object) gameId,
        (object) password
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> ObserveGame(double gameId)
        {
            int Id = this.Invoke("gameService", (object)"observeGame", (object)new object[2]
            {
        (object) gameId,
        null
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> ObserveGame(double gameId, string password)
        {
            int Id = this.Invoke("gameService", (object)"observeGame", (object)new object[2]
            {
        (object) gameId,
        (object) password
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<string> GetSummonerInternalNameByName(string summonerName)
        {
            int Id = this.Invoke("summonerService", (object)"getSummonerInternalNameByName", (object)new object[1]
            {
        (object) summonerName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            string result = (string)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<bool> SwitchTeams(double gameId)
        {
            int Id = this.Invoke("gameService", (object)"switchTeams", (object)new object[1]
            {
        (object) gameId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<bool> SwitchPlayerToObserver(double gameId)
        {
            int Id = this.Invoke("gameService", (object)"switchPlayerToObserver", (object)new object[1]
            {
        (object) gameId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<bool> SwitchObserverToPlayer(double gameId, int team)
        {
            int Id = this.Invoke("gameService", (object)"switchObserverToPlayer", (object)new object[2]
            {
        (object) gameId,
        (object) team
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> QuitGame()
        {
            int Id = this.Invoke("gameService", (object)"quitGame", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public void CreatePracticeGame(PracticeGameConfig practiceGameConfig, GameDTO.Callback callback)
        {
            GameDTO gameDto = new GameDTO(callback);
            this.InvokeWithCallback("gameService", (object)"createPracticeGame", (object)new object[1]
            {
        (object) practiceGameConfig.GetBaseTypedObject()
            }, (RiotGamesObject)gameDto);
        }

        public async Task<GameDTO> CreatePracticeGame(PracticeGameConfig practiceGameConfig)
        {
            int Id = this.Invoke("gameService", (object)"createPracticeGame", (object)new object[1]
            {
        (object) practiceGameConfig.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            GameDTO result = new GameDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> SelectBotChampion(int arg0, BotParticipant arg1)
        {
            int Id = this.Invoke("gameService", (object)"selectBotChampion", (object)new object[2]
            {
        (object) arg0,
        (object) arg1.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> RemoveBotChampion(int arg0, BotParticipant arg1)
        {
            int Id = this.Invoke("gameService", (object)"removeBotChampion", (object)new object[2]
            {
        (object) arg0,
        (object) arg1.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public void StartChampionSelection(double gameId, double optomisticLock, StartChampSelectDTO.Callback callback)
        {
            StartChampSelectDTO startChampSelectDto = new StartChampSelectDTO(callback);
            this.InvokeWithCallback("gameService", (object)"startChampionSelection", (object)new object[2]
            {
        (object) gameId,
        (object) optomisticLock
            }, (RiotGamesObject)startChampSelectDto);
        }

        public async Task<StartChampSelectDTO> StartChampionSelection(double gameId, double optomisticLock)
        {
            int Id = this.Invoke("gameService", (object)"startChampionSelection", (object)new object[2]
            {
        (object) gameId,
        (object) optomisticLock
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            StartChampSelectDTO result = new StartChampSelectDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> SetClientReceivedGameMessage(double gameId, string arg1)
        {
            int Id = this.Invoke("gameService", (object)"setClientReceivedGameMessage", (object)new object[2]
            {
        (object) gameId,
        (object) arg1
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public void GetLatestGameTimerState(double arg0, string arg1, int arg2, GameDTO.Callback callback)
        {
            GameDTO gameDto = new GameDTO(callback);
            this.InvokeWithCallback("gameService", (object)"getLatestGameTimerState", (object)new object[3]
            {
        (object) arg0,
        (object) arg1,
        (object) arg2
            }, (RiotGamesObject)gameDto);
        }

        public async Task<GameDTO> GetLatestGameTimerState(double arg0, string arg1, int arg2)
        {
            int Id = this.Invoke("gameService", (object)"getLatestGameTimerState", (object)new object[3]
            {
        (object) arg0,
        (object) arg1,
        (object) arg2
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            GameDTO result = new GameDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> SelectSpells(int spellOneId, int spellTwoId)
        {
            int Id = this.Invoke("gameService", (object)"selectSpells", (object)new object[2]
            {
        (object) spellOneId,
        (object) spellTwoId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public void SelectDefaultSpellBookPage(SpellBookPageDTO spellBookPage, SpellBookPageDTO.Callback callback)
        {
            SpellBookPageDTO spellBookPageDto = new SpellBookPageDTO(callback);
            this.InvokeWithCallback("spellBookService", (object)"selectDefaultSpellBookPage", (object)new object[1]
            {
        (object) spellBookPage.GetBaseTypedObject()
            }, (RiotGamesObject)spellBookPageDto);
        }

        public async Task<SpellBookPageDTO> SelectDefaultSpellBookPage(SpellBookPageDTO spellBookPage)
        {
            int Id = this.Invoke("spellBookService", (object)"selectDefaultSpellBookPage", (object)new object[1]
            {
        (object) spellBookPage.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SpellBookPageDTO result = new SpellBookPageDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> SelectChampion(int championId)
        {
            int Id = this.Invoke("gameService", (object)"selectChampion", (object)new object[1]
            {
        (object) championId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> SelectChampionSkin(int championId, int skinId)
        {
            int Id = this.Invoke("gameService", (object)"selectChampionSkin", (object)new object[2]
            {
        (object) championId,
        (object) skinId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> ChampionSelectCompleted()
        {
            int Id = this.Invoke("gameService", (object)"championSelectCompleted", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> SetClientReceivedMaestroMessage(double arg0, string arg1)
        {
            int Id = this.Invoke("gameService", (object)"setClientReceivedMaestroMessage", (object)new object[2]
            {
        (object) arg0,
        (object) arg1
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public void RetrieveInProgressSpectatorGameInfo(string summonerName, PlatformGameLifecycleDTO.Callback callback)
        {
            PlatformGameLifecycleDTO gameLifecycleDto = new PlatformGameLifecycleDTO(callback);
            this.InvokeWithCallback("gameService", (object)"retrieveInProgressSpectatorGameInfo", (object)new object[1]
            {
        (object) summonerName
            }, (RiotGamesObject)gameLifecycleDto);
        }

        public async Task<PlatformGameLifecycleDTO> RetrieveInProgressSpectatorGameInfo(string summonerName)
        {
            int Id = this.Invoke("gameService", (object)"retrieveInProgressSpectatorGameInfo", (object)new object[1]
            {
        (object) summonerName
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            PlatformGameLifecycleDTO result = new PlatformGameLifecycleDTO(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<bool> DeclineObserverReconnect()
        {
            int Id = this.Invoke("gameService", (object)"declineObserverReconnect", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            bool result = (bool)this.results[Id].GetTO("data")["body"];
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> AcceptInviteForMatchmakingGame(double gameId)
        {
            int Id = this.Invoke("matchmakerService", (object)"acceptInviteForMatchmakingGame", (object)new object[1]
            {
        (object) gameId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> AcceptPoppedGame(bool accept)
        {
            int Id = this.Invoke("gameService", (object)"acceptPoppedGame", (object)new object[1]
            {
        (object) accept
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> UpdateProfileIconId(int iconId)
        {
            int Id = this.Invoke("summonerService", (object)"updateProfileIconId", (object)new object[1]
            {
        (object) iconId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> BanUserFromGame(double gameId, double accountId)
        {
            int Id = this.Invoke("gameService", (object)"banUserFromGame", (object)new object[2]
            {
        (object) gameId,
        (object) accountId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> BanObserverFromGame(double gameId, double accountId)
        {
            int Id = this.Invoke("gameService", (object)"banObserverFromGame", (object)new object[2]
            {
        (object) gameId,
        (object) accountId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> BanChampion(int championId)
        {
            int Id = this.Invoke("gameService", (object)"banChampion", (object)new object[1]
            {
        (object) championId
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<ChampionBanInfoDTO[]> GetChampionsForBan()
        {
            int Id = this.Invoke("gameService", (object)"getChampionsForBan", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            ChampionBanInfoDTO[] result = new ChampionBanInfoDTO[this.results[Id].GetTO("data").GetArray("body").Length];
            for (int index = 0; index < this.results[Id].GetTO("data").GetArray("body").Length; ++index)
                result[index] = new ChampionBanInfoDTO((TypedObject)this.results[Id].GetTO("data").GetArray("body")[index]);
            this.results.Remove(Id);
            return result;
        }

        public async Task<SearchingForMatchNotification> AttachToLowPriorityQueue(MatchMakerParams matchMakerParams, string accessToken)
        {
            TypedObject to = new TypedObject((string)null);
            to.Add("LEAVER_BUSTER_ACCESS_TOKEN", (object)accessToken);
            int Id = this.Invoke("matchmakerService", (object)"attachToQueue", (object)new object[2]
            {
        (object) matchMakerParams.GetBaseTypedObject(),
        (object) to
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = this.results[Id].GetTO("data").GetTO("body");
            SearchingForMatchNotification result = new SearchingForMatchNotification(messageBody);
            this.results.Remove(Id);
            return result;
        }

        public async Task<object> AcceptTrade(string SummonerInternalName, int ChampionId)
        {
            int Id = this.Invoke("lcdsChampionTradeService", (object)"attemptTrade", (object)new object[3]
            {
        (object) SummonerInternalName,
        (object) ChampionId,
        (object) true
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> ackLeaverBusterWarning()
        {
            int Id = this.Invoke("clientFacadeService", (object)"ackLeaverBusterWarning", (object)new object[0]);
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public async Task<object> callPersistenceMessaging(SimpleDialogMessageResponse response)
        {
            int Id = this.Invoke("clientFacadeService", (object)"callPersistenceMessaging", (object)new object[1]
            {
        (object) response.GetBaseTypedObject()
            });
            while (!this.results.ContainsKey(Id))
                await Task.Delay(10);
            this.results.Remove(Id);
            return (object)null;
        }

        public delegate void OnConnectHandler(object sender, EventArgs e);

        public delegate void OnLoginQueueUpdateHandler(object sender, int positionInLine);

        public delegate void OnLoginHandler(object sender, string username, string ipAddress);

        public delegate void OnDisconnectHandler(object sender, EventArgs e);

        public delegate void OnMessageReceivedHandler(object sender, object message);

        public delegate void OnErrorHandler(object sender, Error error);
    }
}