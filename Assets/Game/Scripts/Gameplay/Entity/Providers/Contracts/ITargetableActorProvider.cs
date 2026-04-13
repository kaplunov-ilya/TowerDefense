using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Entity.Providers.Contracts
{
    public interface ITargetableActorProvider : IEntityProvider
    {
        Actor Owner { get; }
        EEntityTraits Traits { get; }
        EEntitySide Side { get; }
        
        bool IsAlive { get; }
        
        void SetOwner(Actor actor);
        void SetActive(bool enable);
        void SetTraits(EEntityTraits traits);
        void SetSide(EEntitySide side);
    }
}