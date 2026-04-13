using System.Collections.Generic;
using TowerDefence.Core.Services.Pool.Contract;
using TowerDefence.Gameplay.Entity.Projectiles;
using TowerDefence.Gameplay.Entity.Projectiles.Configs;
using TowerDefence.Gameplay.Entity.Projectiles.Contract;
using UnityEngine;
using UnityEngine.Pool;

namespace TowerDefence.Core.Services.Pool
{
    public sealed class PoolProjectile : IPoolProjectiles
    {
        private readonly Dictionary<ProjectileConfig, ObjectPool<IProjectile>> _pools = new();

        public void RegisterProjectile(ProjectileConfig config)
        {
            if (_pools.ContainsKey(config))
                return;

            var pool = new ObjectPool<IProjectile>(
                createFunc: () => CreateProjectile(config),
                actionOnGet: projectile => projectile.SetEnable(true),
                actionOnRelease: projectile => projectile.SetEnable(false),
                actionOnDestroy: projectile => Object.Destroy(projectile.GameObject),
                defaultCapacity: 10,
                maxSize: 100
                );

            _pools[config] = pool;
        }

        public IProjectile Get(ProjectileConfig config)
        {
            if (_pools.TryGetValue(config, out var pool)) 
                return pool.Get();
            
            // Авто-регистрация если забыли зарегистрировать
            RegisterProjectile(config);
            pool = _pools[config];

            return pool.Get();
        }

        public void Release(IProjectile projectile)
        {
            if (_pools.TryGetValue(projectile.Config, out var pool))
            {
                pool.Release(projectile);
            }
        }

        private IProjectile CreateProjectile(ProjectileConfig config)
        {
            var instance = Object.Instantiate(config.ProjectilePrefab);
            return instance;
        }
    }
}