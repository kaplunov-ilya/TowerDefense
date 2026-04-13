using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Variants;

namespace TowerDefence.Gameplay.Behaviour.Attack.Nodes
{
    [Serializable]
    public sealed class ExecuteAttackNode : Node<BehaviourActorContext>
    {
        private readonly Cooldown _cooldown = new(0);

        private AttackTimeAttribute _attribute;
        private IEntityAnimationProvider _animationProvider;

        public override void Init(BehaviourContext context)
        {
            base.Init(context);

            TypedContext.Actor.AttributesGroup.TryGet<AttackTimeAttribute>(
                typeof(AttackTimeAttribute), out _attribute);

            TypedContext.Actor.ProvidersGroup.TryGet<IEntityAnimationProvider>(
                typeof(IEntityAnimationProvider), out _animationProvider);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if (Status != NodeStatus.Running)
            {
                _cooldown.Reset();
                PlayAnimation();
            }

            _cooldown.SetCooldown(_attribute.Value);
            var result = _cooldown.Tick(deltaTime, multiplier);

            UpdateMultiply(multiplier);

            if (result == NodeStatus.Success)
            {
                EndAttack();
            }

            return ReturnStatus(result);
        }

        private void EndAttack()
        {
            StopAnimation();
            TypedContext.Actor.ActorContext.AttackContext.IsWindUp = false;
            TypedContext.Actor.ActorContext.AttackContext.Projectile = null;
        }

        private void PlayAnimation()
        {
            _animationProvider.SetState(EAnimationState.Attacking, _attribute.Value);
        }

        private void UpdateMultiply(float multiplier)
        {
            _animationProvider.UpdateMultiply(multiplier);
        }

        private void StopAnimation()
        {
            //  _animationProvider.Cancel(EAnimationState.Attacking);
        }

        public override void Abort()
        {
            base.Abort();

            _cooldown.Abort();
            TypedContext.Actor.ActorContext.AttackContext.IsWindUp = false;

            StopAnimation();
        }

        public override INode CloneNode() => new ExecuteAttackNode();
    }
}