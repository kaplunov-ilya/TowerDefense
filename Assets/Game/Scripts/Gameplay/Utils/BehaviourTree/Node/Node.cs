using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node
{
    [Serializable]
    public abstract class Node : INode
    {
        [field: SerializeField] public EAbortMode AbortMode { get; private set; }
        
        private BehaviourContext _behaviourContext;

        protected BehaviourContext Context { get; private set; }

        public NodeStatus Status { get; protected set; }

        public virtual void Init(BehaviourContext context)
        {
            Context = context;
        }

        public abstract NodeStatus Tick(float deltaTime, float multiplier);

        /// <summary> Если нужно, чтобы node начала с нуля </summary>
        public virtual void Abort()
        {
            Status = NodeStatus.Empty;
        }

        protected NodeStatus ReturnStatus(NodeStatus status)
        {
            Status = status;
            return status;
        }

        public abstract INode CloneNode();
    }

    [Serializable]
    public abstract class Node<TContext> : Node where TContext : BehaviourContext
    {
        protected TContext TypedContext { get; private set; }
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            
            if(context is TContext value)
            {
                TypedContext = value;
            }
            else
            {
                Debug.LogError($"[{nameof(Node)}.{nameof(TContext)}] Invalid context type");
            }
        }
    }

    [Serializable]
    public sealed class NodeConfig
    {
        
    }
}