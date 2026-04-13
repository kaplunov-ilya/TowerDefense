using System;
using System.Collections.Generic;
using TowerDefence.Core.Services.TimeService;
using TowerDefence.Core.Services.TimeService.Contracts;
using TowerDefence.Gameplay.Services.MoveService.Contract;
using VContainer;
using VContainer.Unity;
using ITickable = TowerDefence.Core.Services.TimeService.Contracts.ITickable;

namespace TowerDefence.Gameplay.Services.MoveService
{
    public sealed class MoveService : IMoveService, ITickable, IInitializable, IDisposable
    {
        [Inject] private readonly ITimeService _timeService;
        
        private readonly List<IMoveable> _moves = new List<IMoveable>(64);
        
        public void Add(IMoveable moveable)
        {
            _moves.Add(moveable);
        }

        public void Remove(IMoveable moveable)
        {
            _moves.Remove(moveable);
        }

        public void Tick(ECombatPhase phase, float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void Initialize() => _timeService.Register(this, ECombatPhase.Execution);

        public void Dispose() => _timeService.Unregister(this, ECombatPhase.Execution);
    }
}