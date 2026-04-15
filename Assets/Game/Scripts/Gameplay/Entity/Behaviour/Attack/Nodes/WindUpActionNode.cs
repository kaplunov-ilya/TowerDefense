using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator;
using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Variants;

namespace TowerDefence.Gameplay.Behaviour.Attack.Nodes
{
    [Serializable]
    /// <summary> Просчет значения, а также вызов анимаций должен быть отсюда</summary>
    public sealed class WindUpActionNode : Node<BehaviourActorContext>
    {
        private readonly Cooldown _cooldown = new(0);

        private IAttribute _attribute;
        private IEntityAnimationProvider _animationProvider;
        
        private AttackContext AttackContext => TypedContext.Actor.ActorContext.AttackContext;

        public override void Init(BehaviourContext context)
        {
            base.Init(context);

            if (TypedContext.Actor.AttributesGroup.TryGet<WindUpTimeAttribute>(typeof(WindUpTimeAttribute), out var value))
                _attribute = value;

            TypedContext.Actor.ProvidersGroup.TryGet(typeof(IEntityAnimationProvider), out _animationProvider);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if (AttackContext.IsWindUp)
                return ReturnStatus(NodeStatus.Success);

            if (Status != NodeStatus.Running)
            {
                _cooldown.SetCooldown(_attribute.Value);
                _cooldown.Reset();
                PlayAnimation();
                return ReturnStatus(NodeStatus.Running);
            }

            UpdateMultiply(multiplier);
            
            var result = _cooldown.Tick(deltaTime, multiplier);

            if (result == NodeStatus.Success)
            {
                AttackContext.IsWindUp = true;
                StopAnimation();
            }

            return ReturnStatus(result);
        }

        private void PlayAnimation()
        {
            //_projectileSlotProvider.SetBeginSlotProjectile(AttackContext.Projectile);
            _animationProvider.SetState(EAnimationState.WindUp, _attribute.Value);
        }

        private void UpdateMultiply(float multiplier)
        {
            _animationProvider.UpdateMultiply(multiplier);
        }

        private void StopAnimation()
        {
            //_projectileSlotProvider.SetReadyProjectileSlot(AttackContext.Projectile);
        }

        public override void Abort()
        {
            base.Abort();

            _cooldown.Abort();
            AttackContext.IsWindUp = false;
            _animationProvider.Cancel(EAnimationState.WindUp);
        }

        public override INode CloneNode() => new WindUpActionNode();
    }
}