using TowerDefence.Gameplay.Entity.Projectiles.Contract;

namespace TowerDefence.Gameplay.Entity.Providers.Contracts
{
    /// <summary> Делает снаряд видимым, подготавливает для дальнейших анимаций </summary>
    public interface IProjectileSlotProvider : IEntityProvider
    {
        /// <summary> Выставляем на позицию подготовки, например в кучу снарядов, в колчан </summary>
        void SetBeginSlotProjectile(IProjectile projectile);
        
        /// <summary> Выставляем на позицию готовности, условно внутри пушки, на луке </summary>
        void SetReadyProjectileSlot(IProjectile projectile);
    }
}