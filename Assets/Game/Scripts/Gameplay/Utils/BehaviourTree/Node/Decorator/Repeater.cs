using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator
{
    [Serializable]
    public sealed class Repeater : Node
    {
        [SerializeReference, SubclassSelector] private INode _child;

        public Repeater(){}
        
        public Repeater(INode child)
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
            _child.Tick(deltaTime, multiplier);
            return NodeStatus.Running;
        }
        
        public override INode CloneNode() => new Repeater(_child.CloneNode());
    }
}