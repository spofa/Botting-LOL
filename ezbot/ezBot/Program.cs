// Decompiled with JetBrains decompiler
// Type: ezBot.Program
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

using ezBot.Utils;
using PvPNetClient;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ezBot
{
    internal class Program
    {
        public static ArrayList accounts = new ArrayList();
        public static ArrayList accountsNew = new ArrayList();
        public static int maxBots = 1;
        public static bool replaceConfig = false;
        public static string champion = "";
        public static int maxLevel = 30;
        public static bool randomSpell = false;
        public static string spell1 = "flash";
        public static string spell2 = "ignite";
        public static string LoLVersion = "";
        public static bool buyExpBoost = false;
        public static int delay1 = 1;
        public static int delay2 = 1;
        public static int lolHeight = 200;
        public static int lolWidth = 300;
        public static bool LOWPriority = true;
        public static string lolPath;
        public static ezBot ezbot;

        private static void Main(string[] args)
        {
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Console.Title = "ezBot: " + DateTime.Now;
                    Thread.Sleep(1000);
                }
            })).Start();

            Program.LoadConfigs();
            Program.LoadLeagueVersion();
            Console.Title = "ezBot";
            Tools.ConsoleMessage("Config loaded.", ConsoleColor.White);
            if (Program.replaceConfig)
            {
                Tools.ConsoleMessage("Changing Game Config.", ConsoleColor.White);
                Program.ChangeGameConfig();
            }
            Tools.ConsoleMessage("Loading accounts.", ConsoleColor.White);
            Program.LoadAccounts();
            int num = 0;
            foreach (string account in Program.accounts)
            {
                try
                {
                    Program.accountsNew.RemoveAt(0);
                    string[] strArray = account.Split(new string[1] { "|" }, StringSplitOptions.None);
                    ++num;
                    if (strArray[3] != null)
                    {
                        Generator.CreateRandomThread(Program.delay1, Program.delay2);
                        QueueTypes QueueType = (QueueTypes)Enum.Parse(typeof(QueueTypes), strArray[3]);
                        ezbot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), Program.lolPath, QueueType, Program.LoLVersion);
                    }
                    else
                    {
                        Generator.CreateRandomThread(Program.delay1, Program.delay2);
                        QueueTypes QueueType = QueueTypes.ARAM;
                        ezbot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), Program.lolPath, QueueType, Program.LoLVersion);
                    }
                    if (num == Program.maxBots)
                        break;
                    Tools.ConsoleMessage("Maximun bots running: " + (object)Program.maxBots, ConsoleColor.Red);
                }
                catch (Exception)
                {
                    Tools.ConsoleMessage("You may have an issue in your accounts.txt", ConsoleColor.Red);
                    Tools.ConsoleMessage("Acconts structure ACCOUNT|PASSWORD|REGION|QUEUE_TYPE", ConsoleColor.Red);
                    Application.Exit();
                }
            }
        }

        public static void LoadLeagueVersion()
        {
            Program.LoLVersion = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "configs\\version.txt").ReadLine();
        }

        private static void ChangeGameConfig()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Program.lolPath + "Config\\game.cfg");
                fileInfo.IsReadOnly = false;
                fileInfo.Refresh();
                string str = "[General]\nGameMouseSpeed=9\nEnableAudio=0\nUserSetResolution=1\nBindSysKeys=0\nSnapCameraOnRespawn=1\nOSXMouseAcceleration=1\nAutoAcquireTarget=0\nEnableLightFx=0\nWindowMode=2\nShowTurretRangeIndicators=0\nPredictMovement=0\nWaitForVerticalSync=0\nColors=16\nHeight=" + (object)Program.lolHeight + "\nWidth=" + (object)Program.lolWidth + "\nSystemMouseSpeed=0\nCfgVersion=4.13.265\n\n[HUD]\nShowNeutralCamps=0\nDrawHealthBars=0\nAutoDisplayTarget=0\nMinimapMoveSelf=0\nItemShopPrevY=19\nItemShopPrevX=117\nShowAllChannelChat=0\nShowTimestamps=0\nObjectTooltips=0\nFlashScreenWhenDamaged=0\nNameTagDisplay=1\nShowChampionIndicator=0\nShowSummonerNames=0\nScrollSmoothingEnabled=0\nMiddleMouseScrollSpeed=0.5000\nMapScrollSpeed=0.5000\nShowAttackRadius=0\nNumericCooldownFormat=3\nSmartCastOnKeyRelease=0\nEnableLineMissileVis=0\nFlipMiniMap=0\nItemShopResizeHeight=47\nItemShopResizeWidth=455\nItemShopPrevResizeHeight=200\nItemShopPrevResizeWidth=300\nItemShopItemDisplayMode=1\nItemShopStartPane=1\n\n[Performance]\nShadowsEnabled=0\nEnableHUDAnimations=0\nPerPixelPointLighting=0\nEnableParticleOptimizations=0\nBudgetOverdrawAverage=10\nBudgetSkinnedVertexCount=10\nBudgetSkinnedDrawCallCount=10\nBudgetTextureUsage=10\nBudgetVertexCount=10\nBudgetTriangleCount=10\nBudgetDrawCallCount=1000\nEnableGrassSwaying=0\nEnableFXAA=0\nAdvancedShader=0\nFrameCapType=3\nGammaEnabled=1\nFull3DModeEnabled=0\nAutoPerformanceSettings=0\n=0\nEnvironmentQuality=0\nEffectsQuality=0\nShadowQuality=0\nGraphicsSlider=0\n\n[Volume]\nMasterVolume=1\nMusicMute=0\n\n[LossOfControl]\nShowSlows=0\n\n[ColorPalette]\nColorPalette=0\n\n[FloatingText]\nCountdown_Enabled=0\nEnemyTrueDamage_Enabled=0\nEnemyMagicalDamage_Enabled=0\nEnemyPhysicalDamage_Enabled=0\nTrueDamage_Enabled=0\nMagicalDamage_Enabled=0\nPhysicalDamage_Enabled=0\nScore_Enabled=0\nDisable_Enabled=0\nLevel_Enabled=0\nGold_Enabled=0\nDodge_Enabled=0\nHeal_Enabled=0\nSpecial_Enabled=0\nInvulnerable_Enabled=0\nDebug_Enabled=1\nAbsorbed_Enabled=1\nOMW_Enabled=1\nEnemyCritical_Enabled=0\nQuestComplete_Enabled=0\nQuestReceived_Enabled=0\nMagicCritical_Enabled=0\nCritical_Enabled=1\n\n[Replay]\nEnableHelpTip=0";
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(str);
                using (StreamWriter streamWriter = new StreamWriter(Program.lolPath + "Config\\game.cfg"))
                    streamWriter.Write(stringBuilder.ToString());
                fileInfo.IsReadOnly = true;
                fileInfo.Refresh();
            }
            catch (Exception ex)
            {
                Tools.ConsoleMessage("game.cfg Error: If using VMWare Shared Folder, make sure it is not set to Read-Only.\nException:" + ex.Message, ConsoleColor.Red);
            }
        }

        public static void LognNewAccount()
        {
            Program.accountsNew = Program.accounts;
            Program.accounts.RemoveAt(0);
            int num = 0;
            if (Program.accounts.Count == 0)
                Tools.ConsoleMessage("No more acocunts to login", ConsoleColor.Red);
            foreach (string account in Program.accounts)
            {
                string[] strArray = account.Split(new string[1] { "|" }, StringSplitOptions.None);
                ++num;
                if (strArray[3] != null)
                {
                    Generator.CreateRandomThread(Program.delay1, Program.delay2);
                    QueueTypes QueueType = (QueueTypes)Enum.Parse(typeof(QueueTypes), strArray[3]);
                    ezbot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), Program.lolPath, QueueType, Program.LoLVersion);
                }
                else
                {
                    Generator.CreateRandomThread(Program.delay1, Program.delay2);
                    QueueTypes QueueType = QueueTypes.ARAM;
                    ezbot = new ezBot(strArray[0], strArray[1], strArray[2].ToUpper(), Program.lolPath, QueueType, Program.LoLVersion);
                }
                if (num == Program.maxBots)
                    break;
                Tools.ConsoleMessage("Maximun bots running: " + (object)Program.maxBots, ConsoleColor.Red);
            }
        }

        public static void LoadConfigs()
        {
            try
            {
                IniFile iniFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "configs\\settings.ini");
                Program.lolPath = iniFile.Read("GENERAL", "LauncherPath");
                Program.maxBots = Convert.ToInt32(iniFile.Read("GENERAL", "MaxBots"));
                Program.maxLevel = Convert.ToInt32(iniFile.Read("GENERAL", "MaxLevel"));
                Program.randomSpell = Convert.ToBoolean(iniFile.Read("GENERAL", "RandomSpell"));
                Program.spell1 = iniFile.Read("GENERAL", "Spell1").ToUpper();
                Program.spell2 = iniFile.Read("GENERAL", "Spell2").ToUpper();
                Program.delay1 = Convert.ToInt32(iniFile.Read("ACCOUNT", "MinDelay"));
                Program.delay2 = Convert.ToInt32(iniFile.Read("ACCOUNT", "MaxDelay"));
                Program.buyExpBoost = Convert.ToBoolean(iniFile.Read("ACCOUNT", "BuyExpBoost"));
                Program.champion = iniFile.Read("CHAMPIONS", "Champion").ToUpper();
                Program.replaceConfig = Convert.ToBoolean(iniFile.Read("LOLSCREEN", "ReplaceLoLConfig"));
                Program.lolHeight = Convert.ToInt32(iniFile.Read("LOLSCREEN", "SreenHeight"));
                Program.lolWidth = Convert.ToInt32(iniFile.Read("LOLSCREEN", "SreenWidth"));
                Program.LOWPriority = Convert.ToBoolean(iniFile.Read("LOLSCREEN", "LOWPriority"));
            }
            catch (Exception ex)
            {
                Tools.ErrorReport(ex.ToString());
                Application.Exit();
            }
        }

        public static void LoadAccounts()
        {
            TextReader textReader = (TextReader)File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "configs\\accounts.txt");
            string str;
            while ((str = textReader.ReadLine()) != null)
            {
                Program.accounts.Add((object)str);
                Program.accountsNew.Add((object)str);
            }
            textReader.Close();
        }
    }
}