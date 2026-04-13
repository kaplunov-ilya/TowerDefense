using System;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Behaviour.Defence.Contracts;
using TowerDefence.Gameplay.Entity;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;

namespace TowerDefence.Gameplay.Behaviour.Defence
{
    [Serializable]
    public sealed class TargetableBehaviour : Behaviour, ITargetableBehaviour
    {
        private ITargetableActorProvider _targetableActorProvider;
        
        public override Type Type => typeof(ITargetableBehaviour);

        public bool IsAlive => Owner.ActorContext.IsAlive.Value;

        public override void Init(Actor owner)
        {
            base.Init(owner);
            
            owner.ProvidersGroup.TryGet<ITargetableActorProvider>(typeof(ITargetableActorProvider), out _targetableActorProvider);
            
            _targetableActorProvider.SetOwner(owner);
            _targetableActorProvider.SetTraits(owner.ActorConfig.Traits);
        }

        public override void SetEnable()
        {
            base.SetEnable();
            _targetableActorProvider.SetActive(true);
        }

        public override void SetDisable()
        {
            base.SetDisable();
            _targetableActorProvider.SetActive(false);
        }

        public void SetSide(EEntitySide side) => _targetableActorProvider.SetSide(side);
        
        public override IBehaviour CloneBehaviour() => new TargetableBehaviour();
    }
}