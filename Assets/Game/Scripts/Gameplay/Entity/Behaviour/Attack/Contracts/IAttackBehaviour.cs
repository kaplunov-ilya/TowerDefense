using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Contract;
using UniRx;

namespace TowerDefence.Gameplay.Behaviour.Attack.Contracts
{
    public interface IAttackBehaviour : INode, IBehaviour
    {
        ReactiveCommand OnAttack { get; }
        IAttackDelivery AttackDelivery { get; }
        IAttackInitiator AttackInitiator { get; }
    }
}