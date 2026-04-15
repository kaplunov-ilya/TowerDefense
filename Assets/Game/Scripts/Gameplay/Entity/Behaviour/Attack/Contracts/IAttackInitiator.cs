using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using TowerDefence.Gameplay.Behaviour.Contract;

namespace TowerDefence.Gameplay.Behaviour.Attack.Contracts
{
    /// <summary>
    /// через него начинается атака 
    /// </summary>
    public interface IAttackInitiator : IActivity
    {
        AttackSource GetData();
    }
}