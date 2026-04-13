using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Domain;
using UniRx;

namespace TowerDefence.Gameplay.Entity.Providers.Contracts
{
    public interface IEnemyFinderProvider
    {
        ReactiveCommand OnListChanged { get; }
        IReadOnlyList<ITargetableActorProvider> Targets { get; }
        void SetRadius(float radius);
        void SetParams(EEntitySide sideEnemy);
        void RemoveTarget(ITargetableActorProvider target);
    }
}