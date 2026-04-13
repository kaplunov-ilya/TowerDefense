using System;
using TowerDefence.Core.Services.TimeService;
using TowerDefence.Core.Services.TimeService.Contracts;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using VContainer;

namespace TowerDefence.Gameplay.Utils.BehaviourTree
{
    public sealed class BehaviourController : ITickable, IDisposable
    {
        [Inject] private readonly ITimeService _timeService;
        
        private INode _node;
        private bool _isInitialized;

        public BehaviourContext Context { get; private set; } = new();

        public void Init(INode node, BehaviourContext context)
        {
            _node = node;
            Context = context;
            _node.Init(context);
            
            _isInitialized = true;
        }

        public void Enable()
        {
            _timeService.Register(this, ECombatPhase.Decision);
        }

        public void Disable()
        {
            _timeService.Unregister(this, ECombatPhase.Decision);
        }

        public void Tick(ECombatPhase phase, float deltaTime)
        {
            if (!_isInitialized)
                return;
            
            _node.Tick(deltaTime, _timeService.Multiplier);
        }

        public void Abort()
        {
            _node?.Abort();
        }

        public void Dispose()
        {
            _timeService.Unregister(this, ECombatPhase.Decision);
        }
    }
}