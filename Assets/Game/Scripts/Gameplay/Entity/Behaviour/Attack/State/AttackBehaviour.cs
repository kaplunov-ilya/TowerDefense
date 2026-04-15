using System;
using UnityEngine;
using UniRx;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Configs;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Attack.Contracts;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Attack.State
{
    [Serializable]
    public sealed class AttackBehaviourNode : BehaviourNode, IAttackBehaviour
    {
        [SerializeField] private BehaviorTreeConfig _behaviourTreeConfig;
        [SerializeReference, SubclassSelector] private IAttackInitiator _attackInitiator;
        [SerializeReference, SubclassSelector] private IAttackDelivery _attackDelivery;

        public override Type Type => typeof(IAttackBehaviour);
        
        public INode AttackTree { get; set; }

        public ReactiveCommand OnAttack { get; } = new();
        public IAttackInitiator AttackInitiator => _attackInitiator;
        public IAttackDelivery AttackDelivery => _attackDelivery;

        public override void Init(Actor owner)
        {
            base.Init(owner);
            
            AttackInitiator.Init(owner);
            AttackDelivery.Init(owner);

            AttackTree = _behaviourTreeConfig.Create();
        }

        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            
            AttackTree.Init(context);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if(!IsEnable.Value)
                return NodeStatus.Failure;
            
            if(AttackTree == null)
                return NodeStatus.Failure;
            
            var status = AttackTree.Tick(deltaTime, multiplier);

            if (status == NodeStatus.Success)
            {
                
            }
            
            return status;
        }

        private void ExecuteAttack()
        {
            var source = _attackInitiator.GetData();
            _attackDelivery.Deliver(source);
            OnAttack.Execute();
        }

        public override void Abort()
        {
            base.Abort();
            
            AttackTree?.Abort();
        }

        public override IBehaviour CloneBehaviour() => CloneSelf();
        public override INode CloneNode() => CloneSelf();

        private AttackBehaviourNode CloneSelf()
        {
            var node = new AttackBehaviourNode();
            node._behaviourTreeConfig = _behaviourTreeConfig;
            _attackInitiator = (IAttackInitiator)_attackInitiator.CloneActivity();
            _attackDelivery = (IAttackDelivery)_attackDelivery.CloneActivity();
            return node;
        }
    }
}