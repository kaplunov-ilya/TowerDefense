using TowerDefence.Gameplay.Entity.Projectiles;
using TowerDefence.Gameplay.Entity.Projectiles.Configs;
using TowerDefence.Gameplay.Entity.Projectiles.Contract;

namespace TowerDefence.Core.Services.Pool.Contract
{
    public interface IPoolProjectiles
    {
        void RegisterProjectile(ProjectileConfig  config);
        IProjectile Get(ProjectileConfig config);
        void Release(IProjectile projectile);
    }
}