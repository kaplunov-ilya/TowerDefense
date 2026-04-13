using System;
using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using UniRx;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public sealed class EnemyFinderProvider : EntityProvider, IEnemyFinderProvider
    {
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private Collider _selfTargetableCollider;
        
        [SerializeField] private EEntitySide _sideEnemy;

        private readonly List<ITargetableActorProvider> _targets = new(16);
        private readonly Collider[] _colliders = new Collider[16]; 
        
        public override Type EntityProviderType => typeof(IEnemyFinderProvider);
        public IReadOnlyList<ITargetableActorProvider> Targets => _targets;
        public ReactiveCommand OnListChanged { get; private set; } = new();

        public void SetParams(EEntitySide sideEnemy)
        {
            _sideEnemy = sideEnemy;
            
            RefreshList();
        }

        public void SetRadius(float radius)
        {
            _collider.radius = radius;
        }

        public void RemoveTarget(ITargetableActorProvider target)
        {
            _targets.Remove(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            TryAdd(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsEnemyValid(other, out var unit) &&
                !_targets.Contains(unit))
                return;

            _targets.Remove(unit);
            OnListChanged.Execute();
        }

        private void TryAdd(Collider other, bool isWithCalculate = true)
        {
            if (!IsEnemyValid(other, out var unit))
                return;
            
            _targets.Add(unit);
            
            if (isWithCalculate)
                OnListChanged.Execute();
        }

        private void RefreshList()
        {
            _targets.Clear();
            
            Physics.OverlapSphereNonAlloc(transform.position, _collider.radius, _colliders);

            foreach (var other in _colliders)
            {
                TryAdd(other, false);
            }

            OnListChanged.Execute();
        }
        

        private bool IsEnemyValid(Collider other, out ITargetableActorProvider finderUnit)
        {
            ITargetableActorProvider unit = null;
            finderUnit =  null;
            
            if(other == null)
                return false;
            
            var result = other != _selfTargetableCollider && 
                         other.TryGetComponent<ITargetableActorProvider>(out unit) && 
                         unit.IsAlive &&
                         unit.Side == _sideEnemy;
            
            finderUnit = unit;

            return result;
        }
    }
}