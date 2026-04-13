using Core.Logger.Contract;
using Core.Logger.Domain;
using Core.Logger.Native;
using TowerDefence.Core.Configs;
using TowerDefence.Core.Services.TimeService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Core.Bootstrap
{
    public sealed class CoreLifetimeScope : LifetimeScope
    {
        [SerializeField] private CoreSettings _coreSettings;
        [SerializeField] private TimeService _timeService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindModels(builder);
            BindServices(builder);
            
            builder.RegisterEntryPoint<BootUseCase>();
        }

        private void BindModels(IContainerBuilder builder)
        {
            builder.RegisterInstance<ELogLevels>(_coreSettings.ProjectLogLevel);
        }
        
        private void BindServices(IContainerBuilder builder)
        {
            builder.Register<LoggerWithLevels>(Lifetime.Singleton).As<ILoggerCore>();
            builder.RegisterInstance(_timeService).AsImplementedInterfaces();
        }
    }
}