using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Attack.Contracts;
using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Behaviour.Nodes
{
    [Serializable]
    /// <summary> Вызывает древо поведений внутри сущности </summary>
    public sealed class AttackNode : Node
    {
        private IAttackBehaviour _attackBehaviour;
        
        private BehaviourActorContext _actorBehaviourContext;
        private BehaviourActorContext ActorContext => _actorBehaviourContext ??= Context as BehaviourActorContext;
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            
            if (ActorContext.Actor.BehavioursGroup.TryGet<IAttackBehaviour>(typeof(IAttackBehaviour), out var value))
            {
                _attackBehaviour = value as IAttackBehaviour;
            }
            
            _attackBehaviour.Init(context);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if (_attackBehaviour == null)
                return ReturnStatus(NodeStatus.Success);

            var status = _attackBehaviour.Tick(deltaTime, multiplier);
            return ReturnStatus(status);
        }

        public override void Abort()
        {
            base.Abort();
            
            _attackBehaviour?.Abort();
        }

        public override INode CloneNode() => new AttackNode();
    }
}