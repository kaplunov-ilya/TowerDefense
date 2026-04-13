using System;
using System.Collections.Generic;
using TowerDefence.Core.Services.TimeService;
using TowerDefence.Core.Services.TimeService.Contracts;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Behaviour.Effects.Contracts;
using TowerDefence.Gameplay.Entity;
using VContainer;

namespace TowerDefence.Gameplay.Behaviour.Effects
{
    public sealed class EffectsBehaviour : Behaviour, IEffectBehaviour, ITickable
    {
        [Inject] private readonly ITimeService _timeService;

        private readonly Dictionary<Type, IEffect> _effects = new();
        private readonly HashSet<Type> _effectsForRemove = new();

        public override Type Type => typeof(IEffectBehaviour);

        public IReadOnlyDictionary<Type, IEffect> Effects => _effects;

        public override void SetEnable()
        {
            base.SetEnable();
            
            _timeService.Register(this, ECombatPhase.Execution);
        }

        public override void SetDisable()
        {
            base.SetDisable();
            
            _timeService.Unregister(this, ECombatPhase.Execution);
        }

        public void ApplyEffect(IEffect effect, Actor source)
        {
            effect.Apply(source, Owner);
            _effects.Add(effect.GetType(), effect);
        }

        public void Discard(IEffect effect)
        {
            effect.Discard();
            _effectsForRemove.Add(effect.GetType());
        }

        public void Tick(ECombatPhase phase, float deltaTime)
        {
            ProcessRemoval();

            foreach (var effect in _effects.Values)
            {
                effect.Tick(deltaTime);
            }
        }
        
        private void ProcessRemoval()
        {
            foreach (var effect in _effectsForRemove)
            {
                _effects.Remove(effect);
            }
            
            _effectsForRemove.Clear();
        }
        
        public override IBehaviour CloneBehaviour() => new EffectsBehaviour();
    }
}