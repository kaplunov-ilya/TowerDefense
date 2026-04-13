using TowerDefence.Gameplay.Behaviour.Contract;

namespace TowerDefence.Gameplay.Behaviour.Attack.Contracts
{
    public interface IEnemyFinderBehaviour : IBehaviour
    {
        void Refresh();
    }
}