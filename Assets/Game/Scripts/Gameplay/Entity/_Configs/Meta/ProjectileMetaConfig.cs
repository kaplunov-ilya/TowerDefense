using System;
using TowerDefence.Gameplay.Entity.Configs.Meta.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Configs.Meta
{
    [Serializable]
    public sealed class ProjectileMetaConfig : IMetaConfig
    {
        [field:SerializeField] public GameObject Prefab { get; private set; }
        
        public Type MetaType => typeof(ProjectileMetaConfig);
    }
    
    public sealed class ProjectileMeta: IMeta
    {
        public GameObject Prefab { get; private set; }
        
        public Type MetaType => typeof(ProjectileMeta);
    }
}