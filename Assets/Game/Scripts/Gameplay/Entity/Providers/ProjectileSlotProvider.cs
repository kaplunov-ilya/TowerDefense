using System;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public sealed class ProjectileSlotProvider : EntityProvider, IProjectileSlotProvider
    {
        /// <summary> Трансформ для анимации объекта </summary>
        [SerializeField] private Transform _parentProjectile;
        
        /// <summary> Где должен находиться анимационный объект  </summary>
        [SerializeField] private Transform _parentBeginPosition;
        [SerializeField] private Transform _parentReadyPosition;
        
        public override Type EntityProviderType => typeof(IProjectileSlotProvider);

        /*public void SetBeginSlotProjectile(IProjectile projectile)
        {
            projectile.Transform.SetParent(_parentProjectile);

            SetPosition(_parentBeginPosition);
        }

        public void SetReadyProjectileSlot(IProjectile projectile)
        {
            projectile.Transform.SetParent(_parentProjectile);
            
            SetPosition(_parentReadyPosition);
        }*/

        private void SetPosition(Transform statePosition)
        {
            _parentProjectile.SetParent(statePosition);
            _parentProjectile.position = statePosition.position;
            _parentProjectile.rotation = statePosition.rotation;
        }
    }
}