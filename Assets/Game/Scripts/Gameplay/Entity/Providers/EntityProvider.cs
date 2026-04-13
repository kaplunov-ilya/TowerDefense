using System;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public abstract class EntityProvider : MonoBehaviour, IEntityProvider
    {
        public virtual Type EntityProviderType => typeof(IEntityProvider);
    }
}