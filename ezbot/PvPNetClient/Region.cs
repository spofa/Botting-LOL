// Decompiled with JetBrains decompiler
// Type: PvPNetClient.Region
// Assembly: ezBot, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B78F9D0-7802-4B84-A548-D5B6D416D380
// Assembly location: D:\Desktop\ezBot.exe

namespace PvPNetClient
{
  public enum Region
  {
    [UseGarenaValue(false), ServerValue("prod.jp1.lol.riotgames.com"), LoginQueueValue("https://lq.jp1.lol.riotgames.jp/"), LocaleValue("jp_JP")] JP,
    [UseGarenaValue(false), LocaleValue("en_US"), ServerValue("prod.na2.lol.riotgames.com"), LoginQueueValue("https://lq.na2.lol.riotgames.com/")] NA,
    [UseGarenaValue(false), ServerValue("prod.euw1.lol.riotgames.com"), LoginQueueValue("https://lq.euw1.lol.riotgames.com/"), LocaleValue("en_GB")] EUW,
    [LocaleValue("en_GB"), UseGarenaValue(false), ServerValue("prod.eun1.lol.riotgames.com"), LoginQueueValue("https://lq.eun1.lol.riotgames.com/")] EUN,
    [LocaleValue("ko_KR"), LoginQueueValue("https://lq.kr.lol.riotgames.com/"), UseGarenaValue(false), ServerValue("prod.kr.lol.riotgames.com")] KR,
    [ServerValue("prod.br.lol.riotgames.com"), UseGarenaValue(false), LoginQueueValue("https://lq.br.lol.riotgames.com/"), LocaleValue("pt_BR")] BR,
    [UseGarenaValue(false), ServerValue("prod.tr.lol.riotgames.com"), LoginQueueValue("https://lq.tr.lol.riotgames.com/"), LocaleValue("en_US")] TR,
    [UseGarenaValue(false), LoginQueueValue("https://lq.ru.lol.riotgames.com/"), LocaleValue("en_US"), ServerValue("prod.ru.lol.riotgames.com")] RU,
    [UseGarenaValue(false), ServerValue("prod.oc1.lol.riotgames.com"), LoginQueueValue("https://lq.oc1.lol.riotgames.com/"), LocaleValue("en_US")] OCE,
    [ServerValue("prod.la1.lol.riotgames.com"), LocaleValue("en_US"), UseGarenaValue(false), LoginQueueValue("https://lq.la1.lol.riotgames.com/")] LAN,
    [LoginQueueValue("https://lq.la2.lol.riotgames.com/"), UseGarenaValue(false), ServerValue("prod.la2.lol.riotgames.com"), LocaleValue("en_US")] LAS,
    [ServerValue("prod.pbe1.lol.riotgames.com"), LocaleValue("en_US"), LoginQueueValue("https://lq.pbe1.lol.riotgames.com/"), UseGarenaValue(false)] PBE,
    [LoginQueueValue("https://lq.lol.garenanow.com/"), UseGarenaValue(true), ServerValue("prod.lol.garenanow.com"), LocaleValue("en_US")] SG,
    [LocaleValue("en_US"), LoginQueueValue("https://lq.lol.garenanow.com/"), UseGarenaValue(true), ServerValue("prod.lol.garenanow.com")] MY,
    [ServerValue("prod.lol.garenanow.com"), UseGarenaValue(true), LoginQueueValue("https://lq.lol.garenanow.com/"), LocaleValue("en_US")] SGMY,
    [LoginQueueValue("https://loginqueuetw.lol.garenanow.com/"), UseGarenaValue(true), ServerValue("prodtw.lol.garenanow.com"), LocaleValue("en_US")] TW,
    [ServerValue("prodth.lol.garenanow.com"), LocaleValue("en_US"), UseGarenaValue(true), LoginQueueValue("https://lqth.lol.garenanow.com/")] TH,
    [UseGarenaValue(true), ServerValue("prodph.lol.garenanow.com"), LoginQueueValue("https://storeph.lol.garenanow.com/"), LocaleValue("en_US")] PH,
    [LocaleValue("en_US"), ServerValue("prodvn.lol.garenanow.com"), UseGarenaValue(true), LoginQueueValue("https://lqvn.lol.garenanow.com/")] VN,
  }
}
