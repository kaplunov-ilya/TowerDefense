using System;

namespace TowerDefence.Gameplay.Entity.Stats.Contracts
{
    /// <summary> Контракт для стата </summary>
    public interface IAttribute
    {
        float Value { get; }
        IObservable<float> ObserveValue { get; }
        
        void SetBaseValue(float value);
        void AddModifier(StatModifier modifier);
        void RemoveModifier(object source);
    }
}