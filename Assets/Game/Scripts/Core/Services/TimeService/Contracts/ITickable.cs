namespace TowerDefence.Core.Services.TimeService.Contracts
{
    public interface ITickable
    {
        void Tick(ECombatPhase phase, float deltaTime);
    }
}