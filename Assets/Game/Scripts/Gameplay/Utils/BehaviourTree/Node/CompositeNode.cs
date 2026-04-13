using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node
{
    [Serializable]
    public abstract class CompositeNode : Node
    {
        [SerializeReference, SubclassSelector] protected List<INode> _children = new();
        
        protected void SetChildren(List<INode> children) => _children = children;
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);

            foreach (var child in _children)
                child.Init(context); 
        }

        public override void Abort()
        {
            switch (AbortMode)
            {
                case EAbortMode.ActiveOnly:
                    foreach (var child in _children.Where(child => child?.Status == NodeStatus.Running))
                        child.Abort();
                    break;
                case EAbortMode.FullBranch:
                    foreach (var child in _children)
                        child?.Abort();
                    break;
            }
        }

        protected abstract CompositeNode CreateSelf();

        public override INode CloneNode()
        {
            var node = CreateSelf();
            var listChildren = new List<INode>(_children.Count);
            
            foreach (var child in _children)
            {
                listChildren.Add(child.CloneNode());
            }
            
            node.SetChildren(listChildren);
            
            return node;
        }
    }
}