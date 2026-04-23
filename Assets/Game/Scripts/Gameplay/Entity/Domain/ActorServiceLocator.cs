using TowerDefence.Core.Services.MessageBus;
using TowerDefence.Core.Services.TimeService.Contracts;
using VContainer;

namespace TowerDefence.Gameplay.Entity.Domain
{
    public sealed class ActorServiceLocator
    {
        [Inject] public ITimeService TimeService { get; }
        [Inject] public IMessageBus MessageBus { get; }
    }
}