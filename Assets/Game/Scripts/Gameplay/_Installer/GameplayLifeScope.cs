using TowerDefence.Gameplay.Services.AttackApplierService;
using TowerDefence.Gameplay.Services.AttackApplierService.Contract;
using TowerDefence.Core.Services.Pool;
using TowerDefence.Core.Services.Pool.Contract;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Entity.Domain;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Gameplay._Installer
{
    public sealed class GameplayLifeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            BindPools(builder);
            BindModels(builder);
            BindServices(builder);
        }

        private void BindPools(IContainerBuilder builder)
        {
            builder.Register<PoolProjectile>(Lifetime.Singleton).As<IPoolProjectiles>();
        }

        private void BindModels(IContainerBuilder builder)
        {
            builder.Register<BehaviourActorContext>(Lifetime.Transient);
            builder.Register<BehaviourController>(Lifetime.Transient);
        }
        
        private void BindServices(IContainerBuilder builder)
        {
            builder.Register<ApplierAttackService>(Lifetime.Singleton).As<IApplierAttackService>().AsSelf();
        }
    }
}