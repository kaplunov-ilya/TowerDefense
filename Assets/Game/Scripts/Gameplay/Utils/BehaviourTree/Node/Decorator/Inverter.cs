using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator
{
    [Serializable]
    public sealed class Inverter : Node
    {
        [SerializeReference, SubclassSelector] private INode _child;
        
        public Inverter(){}
        
        public Inverter(INode child) => _child = child;

        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            _child.Init(context);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            var result = _child.Tick(deltaTime, multiplier);

            if (result == NodeStatus.Running)
                return ReturnStatus(NodeStatus.Running);
            
            return ReturnStatus(result == NodeStatus.Success ? NodeStatus.Failure : NodeStatus.Success);
        }
        
        public override INode CloneNode() => new Inverter(_child.CloneNode());
    }
}