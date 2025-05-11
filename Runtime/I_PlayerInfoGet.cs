namespace Eloi.UWCWarcraft {
    public interface I_PlayerInfoGet
{
    void GetChampionIdAsInteger(out int serverId, out int championId);
    void GetChampionIdFFFFHHHHHHHH(out string championId);
    void GetChampionIdFocus(out string championIdFocus);
    void GetWindowHandle(out int windowHandle);
    void GetMapLeftRightX(out float mapLeftRightX);
    void GetMapTopBottomY(out float mapTopBottomY);
    void GetCounterClockWiseAngle360(out float counterClockWiseAngle360);
    void GetChampionLevel(out int championLevel);
    void GetChampionXpModulo999999(out int championXpModulo999999);
    void GetChampionLifePercent(out float percentLife);
    void GetExperiencePercent(out float percentXp);
    void GetPartyChampion1Life09Percent(out float party1Life09);
    void GetPartyChampion2Life09Percent(out float party2Life09);
    void GetPartyChampion3Life09Percent(out float party3Life09);
    void GetPartyChampion4Life09Percent(out float party4Life09);
    void GetPetLife09Percent(out float petLife09);
}

}