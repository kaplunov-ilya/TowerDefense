using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Entity.Nodes
{
    [Serializable]
    public sealed class IsDieNode : Node
    {
        private BehaviourActorContext _actorBehaviourContext;
        private BehaviourActorContext BehaviourActorContext => _actorBehaviourContext ??= Context as BehaviourActorContext;
        private ActorContext ActorContext => BehaviourActorContext.Actor.ActorContext;
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if (ActorContext.IsAlive.Value)
                return ReturnStatus(NodeStatus.Failure);
            
            Die();
            return ReturnStatus(NodeStatus.Success);
        }

        private void Die()
        {
            
        }
        
        public override INode CloneNode() => new IsDieNode();
    }
}