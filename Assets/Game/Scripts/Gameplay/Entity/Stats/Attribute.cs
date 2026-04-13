using System;
using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using UniRx;

namespace TowerDefence.Gameplay.Entity.Stats
{
    public class Attribute : IAttribute
    {
        private readonly ReactiveProperty<float> _property = new();
        private readonly List<StatModifier> _modifiers = new();
        
        private float _baseValue;
        
        public float Value => _property.Value;
        public IObservable<float> ObserveValue => _property;
        
        public void SetBaseValue(float value)
        {
            _baseValue = value;
            Recalculate();
        }
        
        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            Recalculate();
        }

        public void RemoveModifier(object source)
        {
            for (int i = _modifiers.Count - 1; i >= 0; i--)
            {
                if (_modifiers[i].Source == source)
                    _modifiers.RemoveAt(i);
            }

            Recalculate();
        }
        
        private void Recalculate()
        {
            float finalValue = _baseValue;
            float percent = 0f;

            for (int i = 0; i < _modifiers.Count; i++)
            {
                var mod = _modifiers[i];

                if (mod.Type == ModifierType.Flat)
                    finalValue += mod.Value;
                else
                    percent += mod.Value;
            }

            finalValue *= (1f + percent);

            _baseValue = finalValue;
            _property.Value = _baseValue;
        }
    }
    
    public enum ModifierType
    {
        Flat,
        Percent
    }

    public struct StatModifier
    {
        public readonly ModifierType Type;
        public readonly float Value;
        public readonly object Source;

        public StatModifier(ModifierType type, float value, object source)
        {
            Type = type;
            Value = value;
            Source = source;
        }
    }
}