using System;
using System.Collections.Generic;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Composite
{
    [Serializable]
    public sealed class Parallel : CompositeNode
    {
        [SerializeField] private EPolicy _successPolicy;
        [SerializeField] private EPolicy _failPolicy;
        
        public Parallel(){}
        
        public Parallel(EPolicy successPolicy, EPolicy failPolicy)
        {
            _successPolicy = successPolicy;
            _failPolicy = failPolicy;
        }
        
        public Parallel Add(INode child)
        {
            _children.Add(child);
            child.Init(Context);

            return this;
        }
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            Status = NodeStatus.Running;
            
            int successCount = 0;
            int failCount = 0;
            
            foreach (var child in _children)
            {
                var state = child.Tick(deltaTime, multiplier);

                if (state == NodeStatus.Success)
                    successCount++;

                if (state == NodeStatus.Failure)
                    failCount++;
            }

            // SUCCESS
            switch (_successPolicy)
            {
                case EPolicy.RequireAll when successCount == _children.Count:
                case EPolicy.RequireOne when successCount > 0:
                    return NodeStatus.Success;
            }

            // FAIL
            switch (_failPolicy)
            {
                case EPolicy.RequireAll when failCount == _children.Count:
                case EPolicy.RequireOne when failCount > 0:
                    return NodeStatus.Failure;
                default:
                    return NodeStatus.Running;
            }
        }

        protected override CompositeNode CreateSelf() => new Parallel(_successPolicy, _failPolicy);
    }
}