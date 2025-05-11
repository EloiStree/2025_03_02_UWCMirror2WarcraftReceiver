namespace Eloi.UWCWarcraft {
    public interface I_TargetInfoGet
{
    void GetTargetLifePercent(out float targetLifePercent);
    void GetTargetPowerPercent(out float targetPowerPercent);
    void GetTargetLevel(out float targetLevel);
    void GetTargetIdFFFFHHHHHHHH(out string targetId);

}

}