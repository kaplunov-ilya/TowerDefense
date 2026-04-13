using System.Collections.Generic;
using TowerDefence.Core.Services.TimeService.Contracts;
using UnityEngine;
using ITickable = TowerDefence.Core.Services.TimeService.Contracts.ITickable;

namespace TowerDefence.Core.Services.TimeService
{
    using Contracts_ITickable = Contracts.ITickable;

    public sealed class TimeService : MonoBehaviour, ITimeService, ITimeServiceSettings
    {
        private readonly Dictionary<ECombatPhase, List<Contracts_ITickable>> _tickables = new(64);
        private readonly List<(ECombatPhase, Contracts_ITickable)> _tickablesForRemove = new(16);
        private readonly List<ECombatPhase> _combatPhasesPriority = new()
        {
            ECombatPhase.Default,
            ECombatPhase.Decision,
            ECombatPhase.Execution,
        };
        

        private bool _isRunning;
        public float Multiplier { get; private set; } = 1; 
            
        public float DeltaTime { get; private set; }
        

        public void Register(Contracts_ITickable tickable, ECombatPhase combatPhase)
        {
            var list = GetTickables(combatPhase);
            list.Add(tickable);
        }

        public void Unregister(Contracts_ITickable tickable, ECombatPhase combatPhase)
        {
            _tickablesForRemove.Add((combatPhase, tickable));
        }

        public void SetMultipliers(float multiplier)
        {
            Multiplier = multiplier;
        }

        public void StartTime()
        {
            _isRunning = true;
        }

        public void StopTime()
        {
            _isRunning = false;
        }

        private void Update()
        {
            if(!_isRunning)
                return;

            ClearUnsubscribers();

            DeltaTime = Time.deltaTime * Multiplier;

            foreach (var phase in _combatPhasesPriority)
            {
                var list = GetTickables(phase);
                
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Tick(phase, DeltaTime);
                }
            }
        }

        private void ClearUnsubscribers()
        {
            foreach (var tick in _tickablesForRemove)
            {
                var list = GetTickables(tick.Item1);
                list.Remove(tick.Item2);
            }

            _tickablesForRemove.Clear();
        }

        private List<Contracts_ITickable> GetTickables(ECombatPhase phase)
        {
            if (!_tickables.TryGetValue(phase, out var list))
            {
                list = new List<Contracts_ITickable>(32);
                _tickables[phase] = list;
            }
    
            return list;
        }
    }
}