using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Effects.Contracts;
using TowerDefence.Gameplay.Behaviour.Effects.Variants;
using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Entity.Nodes
{
    [Serializable]
    public sealed class HasStunNode : Node
    {
        private readonly Type _stunType = typeof(StunEffect);
        
        private IEffectBehaviour _effectBehaviour;
        
        private BehaviourActorContext _actorBehaviourContext;
        private BehaviourActorContext ActorContext => _actorBehaviourContext ??= Context as BehaviourActorContext;
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            
            if (ActorContext.Actor.BehavioursGroup.TryGet<IEffectBehaviour>(typeof(IEffectBehaviour), out var value))
            {
                _effectBehaviour = value as IEffectBehaviour;
            }
        }
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if (_effectBehaviour == null)
                return ReturnStatus(NodeStatus.Failure);

            if (_effectBehaviour.Effects.ContainsKey(_stunType))
            {
                Context.Controller.Abort();
                Stun();
                return ReturnStatus(NodeStatus.Success);
            }
            
            return ReturnStatus(NodeStatus.Failure);
        }

        public override INode CloneNode() => new HasStunNode();

        private void Stun()
        {
            
        }
    }
}