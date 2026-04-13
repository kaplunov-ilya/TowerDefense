using System;

namespace TowerDefence.Gameplay.Entity.Stats.Contracts
{   /// <summary> Это класс для изменчевых состояний, таких как HP, мана </summary>
    public interface IResource
    {
        /// <summary>Атрибут с которым должен будет работать класс</summary>
        Type AttributeType { get; }
        IObservable<float> ObserveCurrent { get; }
        IObservable<float> ObserveMaxValue { get; }
        float Current { get; }
        float MaxValue { get; }

        void SetAttribute(Attribute maxAttribute);
        void Modify(float value);
    }
}