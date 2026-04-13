using System;
using System.Collections.Generic;
using TowerDefence.Gameplay.Behaviour.Attack.Contracts;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Behaviour.Defence.Contracts;
using TowerDefence.Gameplay.Entity;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Variants;
using UniRx;

namespace TowerDefence.Gameplay.Behaviour.Attack.State
{
    [Serializable]
    public sealed class EnemyFinderBehaviour : Behaviour, IEnemyFinderBehaviour
    {
        private sealed class Target
        {
            public ITargetableActorProvider ActorProvider;
            public int Score;
            
            public void Clear()
            {
                ActorProvider = null;
                Score = 0;
            }
        }

        private readonly Target _bestTarget = new();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly List<ITargetableActorProvider> _unValidTargets = new List<ITargetableActorProvider>(8);
        
        private CompositeDisposable _disposableEnemy = new CompositeDisposable();
        
        private EEntityTraits[] _priorityTraits;
        
        private IEnemyFinderProvider _enemyFinderProvider;
        private AttackRangeAttribute _rangeAttribute;
        
        public override Type Type => typeof(IEnemyFinderBehaviour);

        public override void Init(Actor owner)
        {
            base.Init(owner);
            
            Owner.ProvidersGroup.TryGet<IEnemyFinderProvider>(typeof(IEnemyFinderProvider), out _enemyFinderProvider);
            Owner.AttributesGroup.TryGet<AttackRangeAttribute>(typeof(AttackRangeAttribute), out _rangeAttribute);

            _priorityTraits = Owner.ActorConfig.EnemyPriority;
            
            _enemyFinderProvider.SetRadius(_rangeAttribute.Value);
            _enemyFinderProvider.SetParams(Owner.ActorContext.Side);

            SubscribeOnChanges();
        }

        public void Refresh()
        {
            foreach (var target in  _enemyFinderProvider.Targets)
            {
                if (!target.IsAlive)
                    _unValidTargets.Add(target);
            }

            foreach (var target in _unValidTargets)
            {
                _enemyFinderProvider.RemoveTarget(target);
            }
            
            _unValidTargets.Clear();
            _bestTarget.Clear();

            SelectBestTarget();
        }

        private void SubscribeOnChanges()
        {
            _rangeAttribute.ObserveValue.Subscribe(_enemyFinderProvider.SetRadius).AddTo(_disposables);
            _enemyFinderProvider.OnListChanged.Subscribe(OnListChanged).AddTo(_disposables);
        }

        private void OnListChanged(Unit _)
        {
            SelectBestTarget();
        }
        
        private void SelectBestTarget()
        {
            ITargetableActorProvider bestTarget = null;
            int bestScore = int.MinValue;

            var targets = _enemyFinderProvider.Targets;

            for (int i = 0; i < targets.Count; i++)
            {
                var unit = targets[i];
                int score = CalculateScore(unit.Traits);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestTarget = unit;
                }
            }

            if (bestTarget == null)
            {
                Owner.ActorContext.AttackContext.Target = null;
                _disposableEnemy?.Dispose();
                return;
            }
            
            if(_bestTarget.ActorProvider != null && _bestTarget.Score > bestScore)
                return;
            
            bestTarget.Owner.BehavioursGroup.TryGet<ITargetableBehaviour>(typeof(ITargetableBehaviour), out var target);
            
            _bestTarget.Score = bestScore;
            _bestTarget.ActorProvider = bestTarget;
            
            Owner.ActorContext.AttackContext.Target = target;
        }
        
        private int CalculateScore(EEntityTraits traits)
        {
            int score = 0;

            for (int i = 0; i < _priorityTraits.Length; i++)
            {
                if ((traits & _priorityTraits[i]) != 0)
                {
                    // чем раньше в списке — тем выше вес
                    score += (_priorityTraits.Length - i);
                }
            }

            return score;
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            _disposables?.Dispose();
            _disposableEnemy?.Dispose();
        }
        
        public override IBehaviour CloneBehaviour() => new EnemyFinderBehaviour();
    }
}