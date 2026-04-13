using System;
using System.Collections.Generic;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Effects.Contracts
{
    public interface IEffectBehaviour : IBehaviour
    {
        IReadOnlyDictionary<Type, IEffect>  Effects { get; }
        void ApplyEffect(IEffect effect, Actor source);
        void Discard(IEffect effect);
    }
}