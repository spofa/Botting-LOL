// Decompiled with JetBrains decompiler
// Type: ezBot.ezBot
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using ezBot.Utils;
using PvPNetClient;
using PvPNetClient.RiotObjects.Platform.Clientfacade.Domain;
using PvPNetClient.RiotObjects.Platform.Game;
using PvPNetClient.RiotObjects.Platform.Game.Message;
using PvPNetClient.RiotObjects.Platform.Matchmaking;
using PvPNetClient.RiotObjects.Platform.Messaging;
using PvPNetClient.RiotObjects.Platform.Statistics;
using PvPNetClient.RiotObjects.Platform.Summoner;
using PvPNetClient.RiotObjects.Platform.Trade;
using PvPNetClient.RiotObjects.Team.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ezBot
{
    internal class ezBot
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public LoginDataPacket loginPacket = new LoginDataPacket();
        public PvPNetConnection connection = new PvPNetConnection();
        public bool firstTimeInLobby = true;
        public bool firstTimeInQueuePop = true;
        public bool firstTimeInCustom = true;
        public bool firstTimeInPostChampSelect = true;
        public Process exeProcess;
        public string Accountname;
        public string Password;
        public string ipath;

        public string region { get; set; }

        public string sumName { get; set; }

        public double sumId { get; set; }

        public double sumLevel { get; set; }

        public double archiveSumLevel { get; set; }

        public double rpBalance { get; set; }

        public double ipBalance { get; set; }

        public int m_leaverBustedPenalty { get; set; }

        public string m_accessToken { get; set; }

        public QueueTypes queueType { get; set; }

        public QueueTypes actualQueueType { get; set; }

        public ezBot(string username, string password, string reg, string path, QueueTypes QueueType, string LoLVersion)
        {
            this.ipath = path;
            this.Accountname = username;
            this.Password = password;
            this.queueType = QueueType;
            this.region = reg;
            this.connection.OnConnect += new PvPNetConnection.OnConnectHandler(this.connection_OnConnect);
            this.connection.OnDisconnect += new PvPNetConnection.OnDisconnectHandler(this.connection_OnDisconnect);
            this.connection.OnError += new PvPNetConnection.OnErrorHandler(this.connection_OnError);
            this.connection.OnLogin += new PvPNetConnection.OnLoginHandler(this.connection_OnLogin);
            this.connection.OnLoginQueueUpdate += new PvPNetConnection.OnLoginQueueUpdateHandler(this.connection_OnLoginQueueUpdate);
            this.connection.OnMessageReceived += new PvPNetConnection.OnMessageReceivedHandler(this.connection_OnMessageReceived);
            switch (this.region)
            {
                case "EUW":
                    this.connection.Connect(username, password, Region.EUW, LoLVersion);
                    break;
                case "EUNE":
                    this.connection.Connect(username, password, Region.EUN, LoLVersion);
                    break;
                case "NA":
                    this.connection.Connect(username, password, Region.NA, LoLVersion);
                    break;
                case "KR":
                    this.connection.Connect(username, password, Region.KR, LoLVersion);
                    break;
                case "BR":
                    this.connection.Connect(username, password, Region.BR, LoLVersion);
                    break;
                case "OCE":
                    this.connection.Connect(username, password, Region.OCE, LoLVersion);
                    break;
                case "RU":
                    this.connection.Connect(username, password, Region.RU, LoLVersion);
                    break;
                case "TR":
                    this.connection.Connect(username, password, Region.TR, LoLVersion);
                    break;
                case "LAS":
                    this.connection.Connect(username, password, Region.LAS, LoLVersion);
                    break;
                case "LAN":
                    this.connection.Connect(username, password, Region.LAN, LoLVersion);
                    break;
                case "JP":
                    this.connection.Connect(username, password, Region.JP, LoLVersion);
                    break;
            }
            Tools.ConsoleMessage("Connected at " + this.region.ToUpper() + " server.", ConsoleColor.White);
            switch (this.region)
            {
                case "SG":
                case "MY":
                case "SGMY":
                case "TW":
                case "TH":
                case "PH":
                case "VN":
                    Tools.ConsoleMessage("Garena server is not supported on this version of ezBot", ConsoleColor.Red);
                    this.connection.Disconnect();
                    Thread.Sleep(3000);
                    Application.Exit();
                    break;
            }
        }

        public async void connection_OnMessageReceived(object sender, object message)
        {
            if (message is GameDTO)
            {
                GameDTO game = message as GameDTO;
                switch (game.GameState)
                {
                    case "CHAMP_SELECT":
                        this.firstTimeInCustom = true;
                        this.firstTimeInQueuePop = true;
                        if (!this.firstTimeInLobby)
                            break;
                        this.firstTimeInLobby = false;
                        Tools.ConsoleMessage("You are in champion select.", ConsoleColor.White);
                        object obj1 = await this.connection.SetClientReceivedGameMessage(game.Id, "CHAMP_SELECT_CLIENT");
                        if (this.queueType != QueueTypes.ARAM)
                        {
                            int Spell1;
                            int Spell2;
                            if (!Program.randomSpell)
                            {
                                Spell1 = Enums.GetSpell(Program.spell1);
                                Spell2 = Enums.GetSpell(Program.spell2);
                            }
                            else
                            {
                                Random random = new Random();
                                List<int> intList = new List<int>()
                                {
                                    13,
                                    6,
                                    7,
                                    10,
                                    1,
                                    11,
                                    21,
                                    12,
                                    3,
                                    14,
                                    2,
                                    4
                                };
                                int index1 = random.Next(intList.Count);
                                int index2 = random.Next(intList.Count);
                                int num1 = intList[index1];
                                int num2 = intList[index2];
                                if (num1 == num2)
                                {
                                    int index3 = random.Next(intList.Count);
                                    num2 = intList[index3];
                                }
                                Spell1 = Convert.ToInt32(num1);
                                Spell2 = Convert.ToInt32(num2);
                            }
                            object obj2 = await this.connection.SelectSpells(Spell1, Spell2);
                            string championPick = "";
                            int championPickID = 0;
                            championPickID = Enums.GetChampion(Program.champion);
                            championPick = Program.champion;
                            object obj3 = await this.connection.SelectChampion(championPickID);
                            Tools.ConsoleMessage("Selected Champion: " + championPick.ToUpper(), ConsoleColor.DarkYellow);
                            object obj4 = await this.connection.ChampionSelectCompleted();
                        }
                        if (this.queueType != QueueTypes.ARAM)
                            break;
                        int Spell1_1;
                        int Spell2_1;
                        if (!Program.randomSpell)
                        {
                            Spell1_1 = Enums.GetSpell(Program.spell1);
                            Spell2_1 = Enums.GetSpell(Program.spell2);
                        }
                        else
                        {
                            Random random = new Random();
                            List<int> intList = new List<int>()
              {
                13,
                6,
                7,
                10,
                1,
                11,
                21,
                12,
                3,
                14,
                2,
                4
              };
                            int index1 = random.Next(intList.Count);
                            int index2 = random.Next(intList.Count);
                            int num1 = intList[index1];
                            int num2 = intList[index2];
                            if (num1 == num2)
                            {
                                int index3 = random.Next(intList.Count);
                                num2 = intList[index3];
                            }
                            Spell1_1 = Convert.ToInt32(num1);
                            Spell2_1 = Convert.ToInt32(num2);
                        }
                        object obj5 = await this.connection.SelectSpells(Spell1_1, Spell2_1);
                        object obj6 = await this.connection.ChampionSelectCompleted();
                        break;
                    case "POST_CHAMP_SELECT":
                        this.firstTimeInLobby = false;
                        if (!this.firstTimeInPostChampSelect)
                            break;
                        this.firstTimeInPostChampSelect = false;
                        Tools.ConsoleMessage("Waiting for league of legends.", ConsoleColor.White);
                        break;
                    case "IN_QUEUE":
                        Tools.ConsoleMessage("You are in queue.", ConsoleColor.White);
                        break;
                    case "TERMINATED":
                        this.firstTimeInPostChampSelect = true;
                        this.firstTimeInQueuePop = true;
                        break;
                    case "LEAVER_BUSTED":
                        Tools.ConsoleMessage("You have leave buster.", ConsoleColor.White);
                        break;
                    case "JOINING_CHAMP_SELECT":
                        if (!this.firstTimeInQueuePop || !game.StatusOfParticipants.Contains("1"))
                            break;
                        Tools.ConsoleMessage("Match found and accepted.", ConsoleColor.White);
                        this.firstTimeInQueuePop = false;
                        this.firstTimeInLobby = true;
                        object obj7 = await this.connection.AcceptPoppedGame(true);
                        break;
                }
            }
            else if (message.GetType() == typeof(TradeContractDTO))
            {
                TradeContractDTO tradeContractDto = message as TradeContractDTO;
                if (tradeContractDto == null)
                    return;
                switch (tradeContractDto.State)
                {
                }
            }
            else if (message is PlayerCredentialsDto)
            {
                this.firstTimeInPostChampSelect = true;
                PlayerCredentialsDto playerCredentialsDto = message as PlayerCredentialsDto;
                ProcessStartInfo startInfoLOL = new ProcessStartInfo();
                startInfoLOL.CreateNoWindow = false;
                startInfoLOL.WorkingDirectory = this.FindLoLExe();
                startInfoLOL.FileName = "League of Legends.exe";
                startInfoLOL.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + playerCredentialsDto.ServerIp + " " + (object)playerCredentialsDto.ServerPort + " " + playerCredentialsDto.EncryptionKey + " " + (object)playerCredentialsDto.SummonerId + "\"";
                
                if (Process.GetProcessesByName("League of Legends").Length == 0) {
                    this.exeProcess = Process.Start(startInfoLOL);
                    this.exeProcess.Exited += new EventHandler(this.exeProcess_Exited);
                    this.exeProcess.PriorityClass = !Program.LOWPriority ? ProcessPriorityClass.High : ProcessPriorityClass.Idle;
                    this.exeProcess.EnableRaisingEvents = true;
                    Tools.ConsoleMessage("Launching League of Legends.", ConsoleColor.White);
                }
                else
                {
                    Process[] pl = Process.GetProcessesByName("League of Legends");
                    if(pl.Length > 0)
                    {
                        this.exeProcess = pl[0];
                        this.exeProcess.Exited += new EventHandler(this.exeProcess_Exited);
                        this.exeProcess.EnableRaisingEvents = true;
                    }
                    Tools.ConsoleMessage("Waiting for League of Legends to exit.", ConsoleColor.White);
                }
            }
            else
            {
                if (message is GameNotification || message is SearchingForMatchNotification)
                    return;
                if (message is EndOfGameStats)
                {
                    this.EnterQueue();
                }
                else
                {
                    if (!message.ToString().Contains("EndOfGameStats"))
                        return;
                    try
                    {
                        if (this.exeProcess == null)
                            return;
                        EndOfGameStats eog = new EndOfGameStats();
                        Tools.ConsoleMessage("IP: " + this.loginPacket.IpBalance, ConsoleColor.Cyan);
                        this.connection_OnMessageReceived(sender, (object)eog);
                        this.exeProcess.Exited -= new EventHandler(exeProcess_Exited);
                        this.exeProcess.Kill();
                        Thread.Sleep(500);
                        try
                        {
                            if (this.exeProcess.Responding)
                                Process.Start(@"taskkill /F /IM League of Legends.exe");
                        }
                        catch (Exception) { };
                        this.loginPacket = await this.connection.GetLoginDataPacketForUser();
                        this.archiveSumLevel = this.sumLevel;
                        this.sumLevel = this.loginPacket.AllSummonerData.SummonerLevel.Level;
                        if (this.sumLevel == this.archiveSumLevel)
                            return;
                        this.levelUp();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Thread.Sleep(5000);
                        Application.Exit();
                    }
                }
            }
        }

        private async void EnterQueue()
        {
            MatchMakerParams matchParams = new MatchMakerParams();
            if (this.queueType == QueueTypes.BOT_INTO)
                matchParams.BotDifficulty = "INTRO";
            else if (this.queueType == QueueTypes.BOT_BEGINNER)
                matchParams.BotDifficulty = "EASY";
            else if (this.queueType == QueueTypes.BOT_MEDIUM)
                matchParams.BotDifficulty = "MEDIUM";
            else if (this.queueType == QueueTypes.BOT_3x3)
                matchParams.BotDifficulty = "EASY";
            if (this.sumLevel == 3.0 && this.actualQueueType == QueueTypes.NORMAL_5x5)
                this.queueType = this.actualQueueType;
            else if (this.sumLevel == 6.0 && this.actualQueueType == QueueTypes.ARAM)
                this.queueType = this.actualQueueType;
            else if (this.sumLevel == 7.0 && this.actualQueueType == QueueTypes.NORMAL_3x3)
                this.queueType = this.actualQueueType;
            else if (this.sumLevel == 10.0 && this.actualQueueType == QueueTypes.TT_HEXAKILL)
                this.queueType = this.actualQueueType;
            matchParams.QueueIds = new int[1]
            {
        (int) this.queueType
            };
            SearchingForMatchNotification m = await this.connection.AttachToQueue(matchParams);
            if (m.PlayerJoinFailures == null)
            {
                Tools.ConsoleMessage("In Queue: " + this.queueType.ToString() + " as " + this.loginPacket.AllSummonerData.Summoner.Name + ".", ConsoleColor.Cyan);
            }
            else
            {
                foreach (QueueDodger playerJoinFailure in m.PlayerJoinFailures)
                {
                    if (playerJoinFailure.ReasonFailed == "LEAVER_BUSTED")
                    {
                        this.m_accessToken = playerJoinFailure.AccessToken;
                        if (playerJoinFailure.LeaverPenaltyMillisRemaining > this.m_leaverBustedPenalty)
                            this.m_leaverBustedPenalty = playerJoinFailure.LeaverPenaltyMillisRemaining;
                        Tools.ConsoleMessage("LEAVER_BUSTED: " + DateTime.Now.AddMilliseconds(this.m_leaverBustedPenalty), ConsoleColor.Red);
                    }
                    else if (playerJoinFailure.ReasonFailed == "LEAVER_BUSTER_TAINTED_WARNING")
                    {
                        object obj1 = await this.connection.ackLeaverBusterWarning();
                        object obj2 = await this.connection.callPersistenceMessaging(new SimpleDialogMessageResponse()
                        {
                            AccountID = this.loginPacket.AllSummonerData.Summoner.SumId,
                            MsgID = this.loginPacket.AllSummonerData.Summoner.SumId,
                            Command = "ack"
                        });
                        this.connection_OnMessageReceived((object)null, (object)new EndOfGameStats());
                    }
                    else if (playerJoinFailure.ReasonFailed == "QUEUE_DODGER")
                    {
                        this.m_accessToken = playerJoinFailure.AccessToken;
                        this.m_leaverBustedPenalty = playerJoinFailure.DodgePenaltyRemainingTime;
                        try
                        {
                            Tools.ConsoleMessage("QUEUE_DODGER: " + DateTime.Now.AddMilliseconds(this.m_leaverBustedPenalty), ConsoleColor.Red);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.WriteLine("YOLO2");
                        }
                    }
                }
                try
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds((double)this.m_leaverBustedPenalty));
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("YOLO1");
                }
                m = await this.connection.AttachToLowPriorityQueue(matchParams, this.m_accessToken);
                if (m.PlayerJoinFailures == null)
                {
                    Tools.ConsoleMessage("Joined lower priority queue! as " + this.loginPacket.AllSummonerData.Summoner.Name + ".", ConsoleColor.Cyan);
                }
                else
                {
                    Tools.ConsoleMessage("There was an error in joining lower priority queue.\nDisconnecting.", ConsoleColor.White);
                    this.connection.Disconnect();
                    Thread.Sleep(3000);
                    Application.Exit();
                }
            }
        }

        private string FindLoLExe()
        {
            string ipath = this.ipath;
            if (ipath.Contains("notfound"))
                return ipath;
            return Directory.EnumerateDirectories(ipath + "RADS\\solutions\\lol_game_client_sln\\releases\\").OrderBy<string, DateTime>((Func<string, DateTime>)(f => new DirectoryInfo(f).CreationTime)).Last<string>() + "\\deploy\\";
        }

        private async void exeProcess_Exited(object sender, EventArgs e)
        {
            Tools.ConsoleMessage("Restart League of Legends.", ConsoleColor.White);
            this.loginPacket = await this.connection.GetLoginDataPacketForUser();
            if (this.loginPacket.ReconnectInfo != null && this.loginPacket.ReconnectInfo.Game != null)
                this.connection_OnMessageReceived(sender, (object)this.loginPacket.ReconnectInfo.PlayerCredentials);
            else
                this.connection_OnMessageReceived(sender, (object)new EndOfGameStats());
        }

        private async void RegisterNotifications()
        {
            object obj1 = await this.connection.Subscribe("bc", this.connection.AccountID());
            object obj2 = await this.connection.Subscribe("cn", this.connection.AccountID());
            object obj3 = await this.connection.Subscribe("gn", this.connection.AccountID());
        }

        private void connection_OnLoginQueueUpdate(object sender, int positionInLine)
        {
            if (positionInLine <= 0)
                return;
            Tools.ConsoleMessage("Position to login: " + (object)positionInLine, ConsoleColor.White);
        }

        private void connection_OnLogin(object sender, string username, string ipAddress)
        {
            new Thread((ThreadStart)(async () =>
           {
               Tools.ConsoleMessage("Loging into account...", ConsoleColor.White);
               this.RegisterNotifications();
               this.loginPacket = await this.connection.GetLoginDataPacketForUser();
               if (this.loginPacket.AllSummonerData == null)
               {
                   Tools.ConsoleMessage("Summoner not found in account.", ConsoleColor.Red);
                   Tools.ConsoleMessage("Creating Summoner...", ConsoleColor.Red);
                   Random random = new Random();
                   string summonerName = this.Accountname;
                   if (summonerName.Length > 16)
                       summonerName = summonerName.Substring(0, 12) + new Random().Next(1000, 9999).ToString();
                   AllSummonerData sumData = await this.connection.CreateDefaultSummoner(summonerName);
                   this.loginPacket.AllSummonerData = sumData;
                   Tools.ConsoleMessage("Created Summoner: " + summonerName, ConsoleColor.White);
               }
               this.sumLevel = this.loginPacket.AllSummonerData.SummonerLevel.Level;
               string sumName = this.loginPacket.AllSummonerData.Summoner.Name;
               double sumId = this.loginPacket.AllSummonerData.Summoner.SumId;
               this.rpBalance = this.loginPacket.RpBalance;
               this.ipBalance = this.loginPacket.IpBalance;
               if (this.sumLevel >= (double)Program.maxLevel)
               {
                   Tools.ConsoleMessage("Summoner: " + sumName + " is already max level.", ConsoleColor.White);
                   Tools.ConsoleMessage("Log into new account.", ConsoleColor.White);
                   this.connection.Disconnect();
                   Program.LognNewAccount();
               }
               else
               {
                   if (this.rpBalance == 400.0 && Program.buyExpBoost)
                   {
                       Tools.ConsoleMessage("Buying XP Boost", ConsoleColor.White);
                       try
                       {
                           new Task(new Action(this.buyBoost)).Start();
                       }
                       catch (Exception ex)
                       {
                           Tools.ConsoleMessage("Couldn't buy RP Boost.\n" + ex.Message.ToString(), ConsoleColor.White);
                       }
                   }
                   if (this.sumLevel < 3.0 && this.queueType == QueueTypes.NORMAL_5x5)
                   {
                       Tools.ConsoleMessage("Need to be Level 3 before NORMAL_5x5 queue.", ConsoleColor.White);
                       Tools.ConsoleMessage("Joins Co-Op vs AI (Beginner) queue until 3", ConsoleColor.White);
                       this.queueType = QueueTypes.BOT_BEGINNER;
                       this.actualQueueType = QueueTypes.NORMAL_5x5;
                   }
                   else if (this.sumLevel < 6.0 && this.queueType == QueueTypes.ARAM)
                   {
                       Tools.ConsoleMessage("Need to be Level 6 before ARAM queue.", ConsoleColor.White);
                       Tools.ConsoleMessage("Joins Co-Op vs AI (Beginner) queue until 6", ConsoleColor.White);
                       this.queueType = QueueTypes.BOT_BEGINNER;
                       this.actualQueueType = QueueTypes.ARAM;
                   }
                   else if (this.sumLevel < 7.0 && this.queueType == QueueTypes.NORMAL_3x3)
                   {
                       Tools.ConsoleMessage("Need to be Level 7 before NORMAL_3x3 queue.", ConsoleColor.White);
                       Tools.ConsoleMessage("Joins Co-Op vs AI (Beginner) queue until 7", ConsoleColor.White);
                       this.queueType = QueueTypes.BOT_BEGINNER;
                       this.actualQueueType = QueueTypes.NORMAL_3x3;
                   }
                   Tools.ConsoleMessage("Welcome " + this.loginPacket.AllSummonerData.Summoner.Name + " - lvl (" + (object)this.loginPacket.AllSummonerData.SummonerLevel.Level + ") IP: (" + this.ipBalance.ToString() + ")", ConsoleColor.White);
                   PlayerDTO player = await this.connection.CreatePlayer();
                   if (this.loginPacket.ReconnectInfo != null && this.loginPacket.ReconnectInfo.Game != null)
                       this.connection_OnMessageReceived(sender, (object)this.loginPacket.ReconnectInfo.PlayerCredentials);
                   else
                       this.connection_OnMessageReceived(sender, (object)new EndOfGameStats());
               }
           })).Start();
        }

        private void connection_OnError(object sender, Error error)
        {
            if (error.Message.Contains("is not owned by summoner"))
                return;
            if (error.Message.Contains("Your summoner level is too low to select the spell"))
            {
                Random random = new Random();
                List<int> intList = new List<int>()
        {
          13,
          6,
          7,
          10,
          1,
          11,
          21,
          12,
          3,
          14,
          2,
          4
        };
                int index1 = random.Next(intList.Count);
                int index2 = random.Next(intList.Count);
                int num1 = intList[index1];
                int num2 = intList[index2];
                if (num1 == num2)
                {
                    int index3 = random.Next(intList.Count);
                    num2 = intList[index3];
                }
                Convert.ToInt32(num1);
                Convert.ToInt32(num2);
            }
            else
            {
                Tools.ConsoleMessage("error received:\n" + error.Message, ConsoleColor.White);
                Application.Exit();
            }
        }

        private void connection_OnDisconnect(object sender, EventArgs e)
        {
            Tools.ConsoleMessage("Disconnected", ConsoleColor.White);
            Application.Exit();
        }

        private void connection_OnConnect(object sender, EventArgs e)
        {
        }

        private async void buyBoost()
        {
            try
            {
                if (this.region == "EUW")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.euw1.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.euw1.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "EUNE")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.eun1.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.eun1.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "NA")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.na2.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.na2.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "KR")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.kr.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.kr.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "BR")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.br.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.br.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "RU")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.ru.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.ru.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "TR")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.tr.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.tr.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "LAS")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.la2.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.la2.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "LAN")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.la1.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.la1.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else if (this.region == "OCE")
                {
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.oc1.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.oc1.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
                else
                {
                    if (!(this.region == "JP"))
                        return;
                    string url = await this.connection.GetStoreUrl();
                    HttpClient httpClient = new HttpClient();
                    Console.WriteLine(url);
                    string stringAsync1 = await httpClient.GetStringAsync(url);
                    string storeURL = "https://store.jp1.lol.riotgames.com/store/tabs/view/boosts/1";
                    string stringAsync2 = await httpClient.GetStringAsync(storeURL);
                    string purchaseURL = "https://store.jp1.lol.riotgames.com/store/purchase/item";
                    HttpContent httpContent = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("item_id", "boosts_2"),
            new KeyValuePair<string, string>("currency_type", "rp"),
            new KeyValuePair<string, string>("quantity", "1"),
            new KeyValuePair<string, string>("rp", "260"),
            new KeyValuePair<string, string>("ip", "null"),
            new KeyValuePair<string, string>("duration_type", "PURCHASED"),
            new KeyValuePair<string, string>("duration", "3")
          });
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(purchaseURL, httpContent);
                    Tools.ConsoleMessage("Bought 'XP Boost: 3 Days'!", ConsoleColor.White);
                    httpClient.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                Application.Exit();
            }
        }

        public void levelUp()
        {
            Tools.ConsoleMessage("Level Up: " + (object)this.sumLevel, ConsoleColor.Yellow);
            this.rpBalance = this.loginPacket.RpBalance;
            this.ipBalance = this.loginPacket.IpBalance;
            Tools.ConsoleMessage("Your Current IP: " + (object)this.ipBalance, ConsoleColor.Yellow);
            if (this.sumLevel >= (double)Program.maxLevel)
            {
                Tools.ConsoleMessage("Your character reached the max level: " + (object)Program.maxLevel, ConsoleColor.Red);
                this.connection.Disconnect();
            }
            else
            {
                if (this.rpBalance != 400.0 || !Program.buyExpBoost)
                    return;
                Tools.ConsoleMessage("Buying XP Boost", ConsoleColor.White);
                try
                {
                    new Task(new Action(this.buyBoost)).Start();
                }
                catch (Exception ex)
                {
                    Tools.ConsoleMessage("Couldn't buy RP Boost.\n" + (object)ex, ConsoleColor.Red);
                }
            }
        }

        private string RandomString(int size)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < size; ++index)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * ezBot.random.NextDouble() + 65.0)));
                stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }
    }
}