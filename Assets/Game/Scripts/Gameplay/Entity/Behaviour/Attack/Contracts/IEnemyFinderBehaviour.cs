using TowerDefence.Gameplay.Behaviour.Contract;

namespace TowerDefence.Gameplay.Behaviour.Attack.Contracts
{
    public interface IEnemyFinderBehaviour : IBehaviour
    {
        void Refresh();
        
        /// <summary> Для контроля момента, когда враг попадает в контекст </summary>
        void UpdateContext();
    }
}