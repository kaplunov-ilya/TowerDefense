using System;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using UniRx;

namespace TowerDefence.Gameplay.Entity.Stats
{
    [Serializable]
    public sealed class Resource : IResource, IDisposable
    {
        private readonly ReactiveProperty<float> _property = new();
        
        private Attribute _maxStat;
        private float _current;
        
        private CompositeDisposable _disposables;

        public Type AttributeType { get; }
        
        public IObservable<float> ObserveCurrent => _property;
        public IObservable<float> ObserveMaxValue => _maxStat.ObserveValue;
        public float Current => _current;
        public float MaxValue => _maxStat.Value;

        public Resource(Type attributeType)
        {
            AttributeType = attributeType;
        }

        public void SetAttribute(Attribute maxAttribute)
        {
            _maxStat = maxAttribute;
            _current = maxAttribute.Value;
            
            _disposables?.Dispose();
            _maxStat.ObserveValue.Subscribe(OnMaxChanged).AddTo(_disposables);
        }

        public void Modify(float value)
        {
            var result = _current + value;
            _current = Math.Clamp(result, 0, MaxValue);
            _property.Value = _current;
        }

        private void OnMaxChanged(float value)
        {
            _current = Math.Clamp(_current, 0, value);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}