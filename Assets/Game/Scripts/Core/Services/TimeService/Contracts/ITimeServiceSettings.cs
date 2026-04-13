namespace TowerDefence.Core.Services.TimeService.Contracts
{
    public interface ITimeServiceSettings
    {
        void SetMultipliers(float multiplier);
        void StartTime();
        void StopTime();
    }
}