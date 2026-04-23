using Core.Logger.Contract;
using Core.Logger.Native;
using TowerDefence.Core.Services.Pool;
using TowerDefence.Core.Services.Pool.Contract;
using TowerDefence.Core.Services.TimeService;
using TowerDefence.Core.Services.TimeService.Contracts;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Factory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Gameplay.Entity.Configs
{
    public sealed class Test : LifetimeScope
    {
        [SerializeField] private ActorConfig _actorConfig;
        [SerializeField] private TimeService _timeService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LoggerWithLevels>(Lifetime.Singleton).As<ILoggerCore>();
            builder.RegisterInstance(_timeService).AsImplementedInterfaces();
            builder.Register<PoolProjectile>(Lifetime.Singleton).As<IPoolProjectiles>();
            builder.Register<BehaviourActorContext>(Lifetime.Transient);
            builder.Register<BehaviourController>(Lifetime.Transient);
            builder.Register<FactoryActor>(Lifetime.Transient);
            
            builder.RegisterBuildCallback(con =>
            {
                var factory = con.Resolve<FactoryActor>();
                var timeService = con.Resolve<ITimeServiceSettings>();
            
                var a1 = Create(factory); 
                var a2 = Create(factory); 
                
                a2.BehaviourController.Disable();
                
                a1.View.gameObject.name = "Test";
                
                

                var pos = a1.View.Transform.position;
                pos += new Vector3(2, 0, 0);
                a1.View.Transform.position = pos;
                
                timeService.SetMultipliers(1);
                timeService.StartTime();
            });
        }

        private Actor Create(FactoryActor factory)
        {
            var a1 = factory.CreateActor(_actorConfig);
            a1.SetEnableBehaviours(true);
            a1.BehaviourController.Enable();
            return a1;
        }
    }
}