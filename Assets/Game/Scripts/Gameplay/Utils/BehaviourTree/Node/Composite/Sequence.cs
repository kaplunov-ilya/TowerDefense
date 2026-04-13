using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Composite
{
    [Serializable]
    public sealed class Sequence : CompositeNode
    {
        public Sequence Add(INode child)
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
                    case NodeStatus.Failure:
                        return ReturnStatus(NodeStatus.Failure);
                    case NodeStatus.Running:
                        return ReturnStatus(NodeStatus.Running);
                }
            }
            
            return ReturnStatus(NodeStatus.Success);
        }
        
        protected override CompositeNode CreateSelf() => new Sequence();
    }
}