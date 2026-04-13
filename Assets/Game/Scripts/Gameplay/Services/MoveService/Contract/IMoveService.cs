using TowerDefence.Gameplay.Entity.Projectiles.Contract;

namespace TowerDefence.Gameplay.Services.MoveService.Contract
{
    public interface IMoveService
    {
        void Add(IMoveable moveable);
        void Remove(IMoveable moveable);
    }
}