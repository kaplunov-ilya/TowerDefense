using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator
{
    [Serializable]
    public sealed class RepeatUntilSuccess : Node
    {
        [SerializeReference, SubclassSelector] private INode _child;
        
        public RepeatUntilSuccess(){}

        public RepeatUntilSuccess(INode child)
        {
            _child = child;
        }
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            _child.Init(context);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            var state = _child.Tick(deltaTime, multiplier);

            if (state == NodeStatus.Success)
                return NodeStatus.Success;

            return NodeStatus.Running;
        }
        
        public override INode CloneNode() => new RepeatUntilSuccess(_child.CloneNode());
    }
}