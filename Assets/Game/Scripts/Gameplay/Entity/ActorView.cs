using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Providers;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity
{
    public sealed class ActorView : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public List<EntityProvider> EntityProviders { get; private set; }
    }
}