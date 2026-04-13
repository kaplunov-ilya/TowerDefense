using System;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public sealed class TargetableActorProvider : EntityProvider, ITargetableActorProvider
    {
        public override Type EntityProviderType => typeof(ITargetableActorProvider);

        [field:SerializeField] public EEntityTraits Traits { get; private set; }
        [field:SerializeField] public EEntitySide Side { get; private set; }
        
        public bool IsAlive => Owner.ActorContext.IsAlive.Value;

        public Actor Owner { get; private set; }

        public void SetOwner(Actor actor) => Owner = actor;
        public void SetActive(bool enable) => gameObject.SetActive(enable);
        public void SetTraits(EEntityTraits traits) => Traits =  traits;
        public void SetSide(EEntitySide side) => Side =  side;
    }
}