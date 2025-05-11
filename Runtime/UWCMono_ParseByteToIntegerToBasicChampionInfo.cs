using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.UWCWarcraft {
    public class UWCMono_ParseByteToIntegerToBasicChampionInfo : MonoBehaviour {

        public UWC_ParseByteToIntegerToBasicChampionInfo m_parser;
        
        public void PushInBytesIID(byte[] bytes) {

            if (m_parser == null) return;
            m_parser.PushInBytesIID(bytes);
        }
        public void PushInInteger(int integer)
        {
            if (m_parser == null) return;
            m_parser.PushInInteger(integer);
        }
    }
    [System.Serializable]
    public class UWC_ParseByteToIntegerToBasicChampionInfo 
{

    public Dictionary<int, UWChampionInfoBasic> m_championsFromInteger = new Dictionary<int, UWChampionInfoBasic>();
    public Dictionary<int, UWChampionIntInfo> m_championsFromIntegerInfo = new Dictionary<int, UWChampionIntInfo>();
    public Dictionary<int,  int[]> m_intBytesToHandler = new Dictionary<int, int[]>();
    public Dictionary<int, byte[]> m_intBytesToPlayerGUID = new Dictionary<int, byte[]>();
    public Dictionary<int, byte[]> m_intBytesToTargetGUID = new Dictionary<int, byte[]>();
    public List<UWChampionInfoBasic> m_playerAsInteger = new List<UWChampionInfoBasic>();
    public List<UWChampionIntInfo> m_playerAsIntegerInfoDebug = new List<UWChampionIntInfo>();

    public UnityEvent<UWChampionInfoBasic> m_onChampionUpdated = new UnityEvent<UWChampionInfoBasic>();

    public void Init()
    {
        //m_championsFromInteger.Clear();
        //m_intBytesToPlayerGUID.Clear();
        //m_intBytesToTargetGUID.Clear();
        //m_intBytesToHandler.Clear();
        //m_playerAsInteger.Clear();
        //m_playerAsIntegerInfoDebug.Clear();

    }

    public void PushInBytesIID(byte[] bytes)
    {
        if (bytes == null) return;
        if (bytes.Length == 0) return;


        if (bytes.Length == 4) { 
        
            int value= BitConverter.ToInt32(bytes, 0);
            PushInInteger(value);

        }
        else if (bytes.Length == 8)
        {
            int value=BitConverter.ToInt32(bytes, 4);
            PushInInteger(value);
        }
        else if (bytes.Length == 12)
        {
            int value = BitConverter.ToInt32(bytes, 0);
            PushInInteger(value);
        }
        else if (bytes.Length == 16)
        {
            int value = BitConverter.ToInt32(bytes, 4);
            PushInInteger(value);
        }
    }
   

    public void PushInInteger(int value)
    {

        int playerIndexTag = value / 100000000;

        if (playerIndexTag != 0)
        {
            bool sign = playerIndexTag > 0;
            int absValue = value;
            if (!sign)
            {
                playerIndexTag = -playerIndexTag;
                absValue = -value;
            }
            if (!m_championsFromInteger.ContainsKey(playerIndexTag))
            {
                UWChampionInfoBasic champion = new UWChampionInfoBasic();
                m_championsFromInteger.Add(playerIndexTag, champion);
                m_intBytesToPlayerGUID.Add(playerIndexTag, new byte[6]);
                m_intBytesToTargetGUID.Add(playerIndexTag, new byte[6]);
                m_intBytesToHandler.Add(playerIndexTag, new int[2]);
                m_playerAsInteger = m_championsFromInteger.Values
                    .OrderBy(k => k.m_playerIdFFFFHHHHHHHH)
                    .ToList();
            }
            if (!m_championsFromIntegerInfo.ContainsKey(playerIndexTag))
            {
                UWChampionIntInfo intChampion = new UWChampionIntInfo();
                m_championsFromIntegerInfo.Add(playerIndexTag, intChampion);
                m_playerAsIntegerInfoDebug = m_championsFromIntegerInfo.Values.ToList();
            }

            m_championsFromInteger[playerIndexTag].m_championIndexId = playerIndexTag;
            //101002705 map x
            //102004313 map y
            //103012705
            //104002064
            //-105010405
            //108000549

            int dataType = absValue / 1000000 % 100;
            if (dataType != 0)
            {
                if (dataType == 1)
                {

                    int mapX = value % 1000000;
                    m_championsFromInteger[playerIndexTag].m_mapLeftRightPercentX = mapX / 100f;
                    m_championsFromIntegerInfo[playerIndexTag].m_i01 = value;
                    //Print($"Player {playerIndexTag} mapX {m_championsFromInteger[playerIndexTag].m_mapX}");
                }
                else if (dataType == 2)
                {
                    int mapY = value % 1000000;
                    m_championsFromInteger[playerIndexTag].m_mapTopBottomPercentY = mapY / 100f;
                    m_championsFromIntegerInfo[playerIndexTag].m_i02 = value;
                    //Print($"Player {playerIndexTag} mapY {m_championsFromInteger[playerIndexTag].m_mapY}");
                }
                else if (dataType == 3)
                {
                    int angle360 = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i03 = value;
                    m_championsFromInteger[playerIndexTag].m_counterClockWiseAngle360 = angle360 / 100f;
                    //Print($"Player {playerIndexTag} angle360 {m_championsFromInteger[playerIndexTag].m_angle360}");
                }
                else if (dataType == 4)
                {

                    int worldX = absValue % 1000000;
                    if (!sign)
                        worldX = -worldX;
                    m_championsFromIntegerInfo[playerIndexTag].m_i04 = value;
                    m_championsFromInteger[playerIndexTag].m_worldPositionRightLeftX = worldX;
                }
                else if (dataType == 5)
                {
                    int worldY = absValue % 1000000;
                    if (!sign)
                        worldY = -worldY;
                    m_championsFromIntegerInfo[playerIndexTag].m_i05 = value;
                    m_championsFromInteger[playerIndexTag].m_worldPositionDownTopY = worldY;
                }
                else if (dataType == 6)
                {
                    int playerLevel = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i06 = value;
                    m_championsFromInteger[playerIndexTag].m_championLevel = playerLevel;
                }
                else if (dataType == 7)
                {
                    int playerLifePercent = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i07 = value;
                    m_championsFromInteger[playerIndexTag].m_championPercentLife = playerLifePercent / 10000f;
                    //Print($"Player {playerIndexTag} lifePercent {m_championsFromInteger[playerIndexTag].m_playerLifePercent}");
                }
                else if (dataType == 8)
                {
                    int playerXpPercent = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i08 = value;
                    m_championsFromInteger[playerIndexTag].m_championPercentXp = playerXpPercent / 10000f;
                    //Print($"Player {playerIndexTag} xpPercent {m_championsFromInteger[playerIndexTag].m_playerXpPercent}");
                }


                else if (dataType == 16)
                {
                    int lifePercent = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i16 = value;
                    m_championsFromInteger[playerIndexTag].m_targetLifePercent = lifePercent / 10000f;
                }
                else if (dataType == 17)
                {
                    int powerPercent = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i17 = value;
                    m_championsFromInteger[playerIndexTag].m_targetPowerPercent = powerPercent / 10000f;
                }
                else if (dataType == 21)
                {
                    int xpModulo = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i21 = value;
                    m_championsFromInteger[playerIndexTag].m_championXpModulo999999 = xpModulo;
                }
                else if (dataType == 22)
                {
                    int targetLevel = value % 1000000;

                    m_championsFromIntegerInfo[playerIndexTag].m_i22 = value;
                    m_championsFromInteger[playerIndexTag].m_targetLevel = targetLevel;
                }
               

                else if (dataType == 18)
                {

                    int windowHandle = value % 1000000;

                    m_championsFromIntegerInfo[playerIndexTag].m_i18 = value;
                    m_intBytesToHandler[playerIndexTag][0]= windowHandle;
                    m_championsFromInteger[playerIndexTag].m_windowHandle =
                        m_intBytesToHandler[playerIndexTag][0]
                        + m_intBytesToHandler[playerIndexTag][1] * 1000000;
                }
                else if (dataType == 19)
                {

                    int windowHandle = value % 1000000;

                    m_championsFromIntegerInfo[playerIndexTag].m_i19 = value;
                    m_intBytesToHandler[playerIndexTag][1] = windowHandle;
                    m_championsFromInteger[playerIndexTag].m_windowHandle =
                        m_intBytesToHandler[playerIndexTag][0]
                        + m_intBytesToHandler[playerIndexTag][1] * 1000000;
                }
                else if (dataType == 20)
                {
                    int eventAsInt = value % 1000000;
                    m_championsFromIntegerInfo[playerIndexTag].m_i20 = value;
                    m_championsFromInteger[playerIndexTag].m_onIntegerActionFromGame?.Invoke(eventAsInt);
                }


                else if (dataType == 9)
                {
                    m_championsFromIntegerInfo[playerIndexTag].m_i09 = value;
                    int teamLife = value % 1000000;
                    byte playerLife = (byte)(teamLife / 100000 % 10);
                    byte ally1 = (byte)(teamLife / 10000 % 10);
                    byte ally2 = (byte)(teamLife / 1000 % 10);
                    byte ally3 = (byte)(teamLife / 100 % 10);
                    byte ally4 = (byte)(teamLife / 10 % 10);
                    byte petLife = (byte)(teamLife % 10);

                    m_championsFromInteger[playerIndexTag].m_partyChampion0Life09 = playerLife / 9f;
                    m_championsFromInteger[playerIndexTag].m_partyChampion1Life09 = ally1 / 9f;
                    m_championsFromInteger[playerIndexTag].m_partyChampion2Life09 = ally2 / 9f;
                    m_championsFromInteger[playerIndexTag].m_partyChampion3Life09 = ally3 / 9f;
                    m_championsFromInteger[playerIndexTag].m_partyChampion4Life09 = ally4 / 9f;
                    m_championsFromInteger[playerIndexTag].m_petLife09 = petLife / 9f;

                    // Print($"Player {playerIndexTag} partyLife {m_championsFromInteger[playerIndexTag].m_partyPlayerLifePercent} {m_championsFromInteger[playerIndexTag].m_partyAlly1LifePercent} {m_championsFromInteger[playerIndexTag].m_partyAlly2LifePercent} {m_championsFromInteger[playerIndexTag].m_partyAlly3LifePercent} {m_championsFromInteger[playerIndexTag].m_partyAlly4LifePercent} {m_championsFromInteger[playerIndexTag].m_partyPetLifePercent}");
                }
                else if (dataType == 10)
                {

                    m_championsFromIntegerInfo[playerIndexTag].m_i10 = value;
                    int idPartOne = value % 1000000;
                    int b1 = idPartOne / 1000 % 1000;
                    int b2 = idPartOne % 1000;
                    m_intBytesToPlayerGUID[playerIndexTag][0] = (byte)b1;
                    m_intBytesToPlayerGUID[playerIndexTag][1] = (byte)b2;
                    //Print($"Player {playerIndexTag} idPartOne {m_championsFromInteger[playerIndexTag].m_playerGuid[0]} {m_championsFromInteger[playerIndexTag].m_playerGuid[1]}");

                    TurnBytesToGUID(
                        m_intBytesToPlayerGUID[playerIndexTag]
                        ,out m_championsFromInteger[playerIndexTag].
                        m_playerGuid
                        ,out m_championsFromInteger[playerIndexTag].
                        m_playerIdFFFFHHHHHHHH);
                }
                else if (dataType == 11)
                {
                    m_championsFromIntegerInfo[playerIndexTag].m_i11 = value;
                    int idPartTwo = value % 1000000;
                     int b1 = idPartTwo / 1000 % 1000;
                    int b2 = idPartTwo % 1000;
                    m_intBytesToPlayerGUID[playerIndexTag][2] = (byte)b1;
                    m_intBytesToPlayerGUID[playerIndexTag][3] = (byte)b2;
                    //Print($"Player {playerIndexTag} idPartTwo {m_championsFromInteger[playerIndexTag].m_playerGuid[2]} {m_championsFromInteger[playerIndexTag].m_playerGuid[3]}");
                    TurnBytesToGUID(
                                         m_intBytesToPlayerGUID[playerIndexTag]
                                         , out m_championsFromInteger[playerIndexTag].
                                         m_playerGuid
                                         , out m_championsFromInteger[playerIndexTag].
                                         m_playerIdFFFFHHHHHHHH);
                }
                else if (dataType == 12)
                {

                    m_championsFromIntegerInfo[playerIndexTag].m_i12 = value;
                    int idPartThree = value % 1000000;
                    int b1 = idPartThree / 1000 % 1000;
                    int b2 = idPartThree % 1000;
                    m_intBytesToPlayerGUID[playerIndexTag][4] = (byte)b1;
                    m_intBytesToPlayerGUID[playerIndexTag][5] = (byte)b2;
                    //Print($"Player {playerIndexTag} idPartThree {m_championsFromInteger[playerIndexTag].m_playerGuid[4]} {m_championsFromInteger[playerIndexTag].m_playerGuid[5]}");
                    TurnBytesToGUID(
                                         m_intBytesToPlayerGUID[playerIndexTag]
                                         , out m_championsFromInteger[playerIndexTag].
                                         m_playerGuid
                                         , out m_championsFromInteger[playerIndexTag].
                                         m_playerIdFFFFHHHHHHHH);



                }
                else if (dataType == 13)
                {
                    m_championsFromIntegerInfo[playerIndexTag].m_i13 = value;
                    int idPartOne = value % 1000000;
                    int b1 = idPartOne / 1000 % 1000;
                    int b2 = idPartOne % 1000;
                    m_intBytesToTargetGUID[playerIndexTag][0] = (byte)b1;
                    m_intBytesToTargetGUID[playerIndexTag][1] = (byte)b2;
                    //Print($"Player {playerIndexTag} idPartOne {m_championsFromInteger[playerIndexTag].m_playerGuid[0]} {m_championsFromInteger[playerIndexTag].m_playerGuid[1]}");

                    TurnBytesToGUID(
                     m_intBytesToTargetGUID[playerIndexTag]
                     , out m_championsFromInteger[playerIndexTag].
                     m_targetGuid
                     , out m_championsFromInteger[playerIndexTag].
                     m_playerIdFocusFFFFHHHHHHHH);
                }
                else if (dataType == 14)
                {
                    m_championsFromIntegerInfo[playerIndexTag].m_i14 = value;
                    int idPartTwo = value % 1000000;
                   int b1 = idPartTwo / 1000 % 1000;
                    int b2 = idPartTwo % 1000;
                    m_intBytesToTargetGUID[playerIndexTag][2] = (byte)b1;
                    m_intBytesToTargetGUID[playerIndexTag][3] = (byte)b2;
                    //Print($"Player {playerIndexTag} idPartTwo {m_championsFromInteger[playerIndexTag].m_playerGuid[2]} {m_championsFromInteger[playerIndexTag].m_playerGuid[3]}");

                    TurnBytesToGUID(
                     m_intBytesToTargetGUID[playerIndexTag]
                     , out m_championsFromInteger[playerIndexTag].
                     m_targetGuid
                     , out m_championsFromInteger[playerIndexTag].
                     m_playerIdFocusFFFFHHHHHHHH);
                }
                else if (dataType == 15)
                {
                    m_championsFromIntegerInfo[playerIndexTag].m_i15 = value;
                    int idPartThree = value % 1000000;
                     int b1 = idPartThree / 1000 % 1000;
                    int b2 = idPartThree % 1000;
                    m_intBytesToTargetGUID[playerIndexTag][4] = (byte)b1;
                    m_intBytesToTargetGUID[playerIndexTag][5] = (byte)b2;
                    //Print($"Player {playerIndexTag} idPartThree {m_championsFromInteger[playerIndexTag].m_playerGuid[4]} {m_championsFromInteger[playerIndexTag].m_playerGuid[5]}");

                    TurnBytesToGUID(
                     m_intBytesToTargetGUID[playerIndexTag]
                     , out m_championsFromInteger[playerIndexTag].
                     m_targetGuid
                     , out m_championsFromInteger[playerIndexTag].
                     m_playerIdFocusFFFFHHHHHHHH);

                }
                m_onChampionUpdated.Invoke(m_championsFromInteger[playerIndexTag]);
            }

        }

    }

    private void TurnBytesToGUID(byte[] bytes, out byte[] m_playerGuid, out string FFFFHHHHHHHH)
    {
        m_playerGuid = bytes;
        char hexF1, hexF2;
        ByteToFF(m_playerGuid[0], out hexF1, out hexF2);
        FFFFHHHHHHHH = $"{hexF1}{hexF2}";
        ByteToFF(m_playerGuid[1], out hexF1, out hexF2);
        FFFFHHHHHHHH += $"{hexF1}{hexF2}";
        FFFFHHHHHHHH += "-";
        ByteToFF(m_playerGuid[2], out hexF1, out hexF2);
        FFFFHHHHHHHH += $"{hexF1}{hexF2}";
        ByteToFF(m_playerGuid[3], out hexF1, out hexF2);
        FFFFHHHHHHHH += $"{hexF1}{hexF2}";
        ByteToFF(m_playerGuid[4], out hexF1, out hexF2);
        FFFFHHHHHHHH += $"{hexF1}{hexF2}";
        ByteToFF(m_playerGuid[5], out hexF1, out hexF2);
        FFFFHHHHHHHH += $"{hexF1}{hexF2}";

    }

    

    public void ByteToFF(byte b, out char hexF1, out char hexF2)
    {
        int b1 = b / 16 % 16;
        int b2 = b % 16;
        hexF1 = (char)(b1 < 10 ? '0' + b1 : 'A' + b1 - 10);
        hexF2 = (char)(b2 < 10 ? '0' + b2 : 'A' + b2 - 10);
    }


    public void TryToFetchPlayerByID(string championId, out bool found, out UWChampionInfoBasic championsFound)
    {

        found = false;
        championsFound = null;
        List<UWChampionInfoBasic> champions = m_championsFromInteger.Values.ToList();
        foreach (UWChampionInfoBasic champion in champions)
        {
            if (champion == null) continue;
            if (champion.m_playerIdFFFFHHHHHHHH == null) continue;
            if (champion.m_playerIdFFFFHHHHHHHH == championId)
            {
                found = true;
                championsFound = champion;
                return;
            }
        }

        if (!found)
        {
            foreach (UWChampionInfoBasic champion in champions)
            {
                if (champion == null) continue;
                if (champion.m_playerIdFFFFHHHHHHHH == null) continue;
                if (champion.m_playerIdFFFFHHHHHHHH.IndexOf(championId) >= 0)
                {
                    found = true;
                    championsFound = champion;
                    return;
                }
            }


        }

    }

    public void GetChampionsId(out List<string> ids)
    {
        ids = new List<string>();
        List<UWChampionInfoBasic> champions = m_championsFromInteger.Values.ToList();
        foreach (UWChampionInfoBasic champion in champions)
        {
            if (champion == null) continue;
            if (champion.m_playerIdFocusFFFFHHHHHHHH == null) continue;
            ids.Add(champion.m_playerIdFocusFFFFHHHHHHHH);
        }
    }

    public void GetChampions(out List<UWChampionInfoBasic> champions)
    {
        if (m_championsFromInteger == null)
            champions = new List<UWChampionInfoBasic>();
        else

            champions = m_championsFromInteger.Values.ToList();
    }




}

}