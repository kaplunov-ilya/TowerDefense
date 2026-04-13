using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator
{
    [Serializable]
    public sealed class RepeatUntilFailure : Node
    {
        [SerializeReference, SubclassSelector] private INode _child;
        
        public RepeatUntilFailure(){}

        public RepeatUntilFailure(INode child)
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

            if (state == NodeStatus.Failure)
                return NodeStatus.Failure;

            return NodeStatus.Running;
        }
        
        public override INode CloneNode() => new RepeatUntilFailure(_child.CloneNode());
    }
}