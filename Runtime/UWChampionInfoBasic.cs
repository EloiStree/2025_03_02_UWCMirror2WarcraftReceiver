using System;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.UWCWarcraft {
    [System.Serializable]
public class UWChampionInfoBasic: I_TargetInfoGet, I_PlayerInfoGet, I_ChampionIntegerActionFromGame
{
    public string m_playerIdFFFFHHHHHHHH = "FFFF-FFFFFFFF";
    public string m_playerIdFocusFFFFHHHHHHHH = "FFFF-FFFFFFFF";
    public int m_championIndexId;
    public int m_windowHandle;
    public byte[] m_playerGuid = new byte[6];
    public byte[] m_targetGuid = new byte[6];

    public float m_worldPositionDownTopY;
    public float m_worldPositionRightLeftX;
    [Range(0, 100)]
    public float m_mapLeftRightPercentX;
    [Range(0, 100)]
    public float m_mapTopBottomPercentY;
    [Range(0, 360)]
    public float m_counterClockWiseAngle360;

    public int m_championLevel;
    public int m_championXpModulo999999;

    [Range(0, 1)]
    public float m_championPercentLife = 0;
    [Range(0, 1)]
    public float m_championPercentXp = 0;


    [Range(0, 1)]
    public float m_partyChampion0Life09;
    [Range(0, 1)]
    public float m_partyChampion1Life09;
    [Range(0, 1)]
    public float m_partyChampion2Life09;
    [Range(0, 1)]
    public float m_partyChampion3Life09;
    [Range(0, 1)]
    public float m_partyChampion4Life09;
    [Range(0, 1)]
    public float m_petLife09;

    [Range(0, 1)]
    public float m_targetLifePercent;
    [Range(0, 1)]
    public float m_targetPowerPercent;
    public float m_targetLevel;

    public int m_lastIntegerActionFromGame;

    public UnityEvent<int> m_onIntegerActionFromGame = new UnityEvent<int>();
    public void AddIntegerToActionFromGame(Action<int> gameIntegerAction)
    {

        if (gameIntegerAction == null) return;
        m_onIntegerActionFromGame.AddListener(gameIntegerAction.Invoke);
    }
    public void RemoveIntegerToActionFromGame(Action<int> gameIntegerAction)
    {
        if (gameIntegerAction == null) return;
        m_onIntegerActionFromGame.RemoveListener(gameIntegerAction.Invoke);
    }
    public void PushIntegerToActionFromGame(int value)
    {
        m_lastIntegerActionFromGame = value;
        m_onIntegerActionFromGame?.Invoke(value);
    }
    public void GetChampionLevel(out int championLevel)
    => championLevel = m_championLevel;

    public void GetChampionXpModulo999999(out int championXpModulo999999)
       => championXpModulo999999 = m_championXpModulo999999%1000000;
    public void GetCounterClockWiseAngle360(out float counterClockWiseAngle360)
    => counterClockWiseAngle360 = m_counterClockWiseAngle360%360.0f;
    public void GetMapLeftRightX(out float mapLeftRightX)
    => mapLeftRightX = m_mapLeftRightPercentX;
    public void GetMapTopBottomY(out float mapTopBottomY)
    => mapTopBottomY = m_mapTopBottomPercentY;
    public void GetPartyChampion1Life09Percent(out float partyPlayer1Life09)
    => partyPlayer1Life09 = m_partyChampion1Life09;
    public void GetPartyChampion2Life09Percent(out float partyPlayer2Life09)
    => partyPlayer2Life09 = m_partyChampion2Life09;

    public void GetPartyChampion3Life09Percent(out float partyPlayer3Life09)
    => partyPlayer3Life09 = m_partyChampion3Life09;
    public void GetPartyChampion4Life09Percent(out float partyPlayer4Life09)
    => partyPlayer4Life09 = m_partyChampion4Life09;

    public void GetChampionLifePercent(out float percentLife)
   => percentLife = m_championPercentLife;

    public void GetExperiencePercent(out float percentXp)
     => percentXp = m_championPercentXp;
    public void GetPetLife09Percent(out float petLife09)
    => petLife09 = m_petLife09;

    public void GetChampionIdFFFFHHHHHHHH(out string playerId)
        => playerId = m_playerIdFFFFHHHHHHHH;
    public void GetChampionIdFocus(out string playerIdFocus)
    => playerIdFocus = m_playerIdFocusFFFFHHHHHHHH;

    public void GetTargetIdFFFFHHHHHHHH(out string targetId)
    => targetId = m_playerIdFocusFFFFHHHHHHHH;
    public void GetTargetLevel(out float targetLevel)
    => targetLevel = m_targetLevel;
    public void GetTargetLifePercent(out float targetLifePercent)
    => targetLifePercent = m_targetLifePercent;

    public void GetTargetPowerPercent(out float targetPowerPercent)
    => targetPowerPercent = m_targetPowerPercent;

    public void GetWindowHandle(out int windowHandle)
    => windowHandle = m_windowHandle;

    public void GetChampionIdAsInteger(out int serverId, out int championId)
    {
        string t = m_playerIdFFFFHHHHHHHH.Replace("-", "");
        string serverIdString = t.Substring(0, 4);
        string championIdString = t.Substring(4, 8);
        serverId = Convert.ToInt32(serverIdString, 16);
        championId = Convert.ToInt32(championIdString, 16);

    }

}

}