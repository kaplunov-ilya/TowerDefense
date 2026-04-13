using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Configs;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Attack.Contracts;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour.Attack.State
{
    [Serializable]
    public sealed class AttackBehaviourNode : BehaviourNode, IAttackBehaviour
    {
        [SerializeField] private BehaviorTreeConfig _behaviourTreeConfig;
        
        public override Type Type => typeof(IAttackBehaviour);
        
        public INode AttackTree { get; set; }
        
        public void SetBehaviorTreeConfig(BehaviorTreeConfig config) => _behaviourTreeConfig = config; 

        public override void Init(Actor owner)
        {
            base.Init(owner);

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
            return status;
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
            node.SetBehaviorTreeConfig(_behaviourTreeConfig);
            return node;
        }
    }
}