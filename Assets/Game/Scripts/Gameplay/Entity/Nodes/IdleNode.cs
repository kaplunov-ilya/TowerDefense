using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;

namespace TowerDefence.Gameplay.Entity.Nodes
{
    [Serializable]
    public sealed class IdleNode : Node
    {
        private IEntityAnimationProvider _animationProvider;
        
        private BehaviourActorContext _actorBehaviourContext;
        private BehaviourActorContext ActorContext => _actorBehaviourContext ??= Context as BehaviourActorContext;
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            
            ActorContext.Actor.ProvidersGroup.TryGet(typeof(IEntityAnimationProvider), out _animationProvider);
        }
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            Context.Controller.Abort();
            _animationProvider.ForceCancel();
            _animationProvider.SetState(EAnimationState.Idle);
            
            return NodeStatus.Running;
        }
        
        public override INode CloneNode() => new IdleNode();
    }
}