using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using TowerDefence.Gameplay.Behaviour.Contract;

namespace TowerDefence.Gameplay.Behaviour.Attack.Contracts
{
    /// <summary>
    /// находим цели, или реализуем вариант доставки атаки
    /// </summary>
    public interface IAttackDelivery : IActivity
    {
        void Deliver(AttackSource source);
    }
}