using System.Collections.Generic;
using TowerDefence.Gameplay.Behaviour.Defence.Contracts;
using TowerDefence.Gameplay.Behaviour.Effects.Contracts;
using TowerDefence.Gameplay.Entity;
using TowerDefence.Gameplay.Services.AttackApplierService.Contract;

namespace TowerDefence.Gameplay.Services.AttackApplierService
{
    public sealed class ApplierAttackService : IApplierAttackService
    {
        public void ApplyAttack(Actor owner, Actor target, float damage, List<IEffect> effects)
        {
            ApplyEffects(owner, target, effects);
            ApplyDamage(target, damage);
        }

        private static void ApplyDamage(Actor target, float damage)
        {
            if (target.BehavioursGroup.TryGet<IDamageBehaviour>(typeof(IDamageBehaviour), out var damageBehaviour))
            {
                damageBehaviour.TakeAttack(damage);
            }
        }

        private static void ApplyEffects(Actor owner, Actor target, List<IEffect> effects)
        {
            if (target.BehavioursGroup.TryGet<IEffectBehaviour>(typeof(IEffectBehaviour), out var effectBehaviour))
            {
                foreach (var effect in effects)
                {
                    effectBehaviour.ApplyEffect(effect, owner);
                }
            }
        }
    }
}