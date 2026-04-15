using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Entity.Domain;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour.Attack.Nodes
{
    [Serializable]
    /// <summary> Проверяем смотрит ли игрок на таргет </summary>
    public sealed class IsLookAtTargetNode : Node<BehaviourActorContext>
    {
        private const float Threshold = 0.866f; // cos(30°)
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            return ReturnStatus(NodeStatus.Success);
            
            var target = TypedContext.AttackContext.Target.View.Transform.position;
            var transform = TypedContext.Actor.View.Transform;
            
            Vector3 dir = target - transform.position;

            float dot = Vector3.Dot(transform.forward, dir);
            float threshold = Threshold * dir.magnitude;

            var result = dot >= threshold;
            
            return ReturnStatus(result ?  NodeStatus.Success : NodeStatus.Failure);
        }
        
        public override INode CloneNode() => new IsLookAtTargetNode();
    }
}