using TowerDefence.Core.Services.TimeService.Contracts;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Core.Bootstrap
{
    public sealed class BootUseCase : IInitializable
    {
        [Inject] private readonly ITimeServiceSettings _timeServiceSettings;
        
        public void Initialize()
        {
            _timeServiceSettings.StartTime();
        }
    }
}