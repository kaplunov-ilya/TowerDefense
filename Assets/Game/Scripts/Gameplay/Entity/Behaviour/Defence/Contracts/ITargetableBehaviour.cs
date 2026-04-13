using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Behaviour.Defence.Contracts
{
    public interface ITargetableBehaviour : IBehaviour
    {
        bool IsAlive { get; }
        void SetSide(EEntitySide side);
    }
}