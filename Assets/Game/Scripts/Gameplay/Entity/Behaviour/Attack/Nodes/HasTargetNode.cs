using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Behaviour.Attack.Nodes
{
    [Serializable]
    public sealed class HasTargetNode : Node<BehaviourActorContext>
    {
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            var target = TypedContext.AttackContext.Target;
            
            var result = target != null && target.ActorContext.IsAlive.Value == true ? NodeStatus.Success : NodeStatus.Failure;

            if (result == NodeStatus.Failure)
            {
                Context.Controller.Abort();
            }
            
            return ReturnStatus(result);
        }
        
        public override INode CloneNode() => new HasTargetNode();
    }
}