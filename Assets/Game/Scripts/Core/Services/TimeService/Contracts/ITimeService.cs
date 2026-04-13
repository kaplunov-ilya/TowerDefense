namespace TowerDefence.Core.Services.TimeService.Contracts
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float Multiplier { get; }
        
        void Register(ITickable tickable, ECombatPhase combatPhase);
        void Unregister(ITickable tickable, ECombatPhase combatPhase);
    }
}