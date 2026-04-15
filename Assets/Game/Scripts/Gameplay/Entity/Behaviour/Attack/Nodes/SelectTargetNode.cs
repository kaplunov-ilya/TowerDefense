using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Attack.Contracts;
using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Behaviour.Attack.Nodes
{
    [Serializable]
    public sealed class SelectTargetNode : Node<BehaviourActorContext>
    {
        private IEnemyFinderBehaviour _behaviour;
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);

            if (TypedContext.Actor.BehavioursGroup.TryGet(typeof(IEnemyFinderBehaviour), out _behaviour));
        }
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            _behaviour.UpdateContext();
            
            var target = TypedContext.AttackContext.Target;
            
            if ( target == null || target.ActorContext.IsAlive.Value == false)
            {
                _behaviour.Refresh();
            }
            
            return ReturnStatus(NodeStatus.Success);
        }

        public override INode CloneNode() => new SelectTargetNode();
    }
}