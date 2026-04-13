using System;
using TowerDefence.Core.Services.MessageBus;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity;
using UniRx;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour
{
    /// <summary> Базовая реализация поведения актера </summary>
    public abstract class Behaviour : IBehaviour, IDisposable
    {
        protected readonly CompositeDisposable Disposables = new();
        
        protected IMessageBus MessageBus;
        public Actor Owner { get; private set; }
        public ReactiveProperty<bool> IsEnable { get; protected set; } = new();
        
        public abstract Type Type { get; }

        public virtual void Init(Actor owner)
        {
            Owner = owner;
        }
        
        public virtual void SetEnable() => IsEnable.Value = true;
        public virtual void SetDisable() => IsEnable.Value = false;

        public virtual IBehaviour CloneBehaviour() => this;

        public virtual void Dispose()
        {
            Disposables?.Dispose();
            SetDisable();
        }
    }

    /// <summary> Поведение в контексте древа поведений </summary>
    public abstract class BehaviourNode : Behaviour, INode
    {
        [field: SerializeField] public EAbortMode AbortMode { get; private set; }
        
        public Node Parent { get; set; }
        public NodeStatus Status { get; private set; }

        protected BehaviourContext Context { get; private set; }
        
        public virtual void Init(BehaviourContext context)
        {
            Context = context;
        }

        public abstract NodeStatus Tick(float deltaTime, float multiplier);

        public virtual void Abort()
        {
            Status = NodeStatus.Empty;
        }

        public virtual INode CloneNode() => throw new NotImplementedException();


        protected NodeStatus ReturnStatus(NodeStatus status)
        {
            Status = status;
            return status;
        }
    }
}