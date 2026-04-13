using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Composite
{
    [Serializable]
    public sealed class Selector : CompositeNode
    {
        public Selector Add(INode child)
        {
            _children.Add(child);
            child.Init(Context);

            return this;
        }
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            Status = NodeStatus.Running;
            
            foreach (var child in _children)
            {
                var result = child.Tick(deltaTime, multiplier);
                
                switch (result)
                {
                    case NodeStatus.Success:
                        return ReturnStatus(NodeStatus.Success);
                    case NodeStatus.Running:
                        return ReturnStatus(NodeStatus.Running);
                }
            }
            
            return ReturnStatus(NodeStatus.Failure);
        }
        
        protected override CompositeNode CreateSelf() => new Selector();
    }
}