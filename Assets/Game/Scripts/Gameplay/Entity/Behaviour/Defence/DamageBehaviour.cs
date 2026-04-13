using System;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Behaviour.Defence.Contracts;
using UniRx;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour.Defence
{
    [Serializable]
    public sealed class DamageBehaviour : Behaviour, IDamageBehaviour
    {
        private ReactiveProperty<bool> _isAlive = new();

        public Transform Transform { get; }

        public IReadOnlyReactiveProperty<bool> IsAlive => _isAlive;
        
        public override Type Type => typeof(IDamageBehaviour);

        public void TakeAttack(float damage)
        {
            
        }
        
        public override IBehaviour CloneBehaviour() => new DamageBehaviour();
    }
}