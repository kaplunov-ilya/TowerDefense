using System;
using TowerDefence.Gameplay.Entity.Stats.Configs.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Stats.Configs
{
    [Serializable]
    public sealed class ResourceConfig : IResourceConfig
    {
        [SerializeReference, SubclassSelector] private IAttribute _attributeType;
        
        public IResource Create()
        {
            if (_attributeType == null)
                throw new System.Exception("StatType not assigned!");
            
            return new Resource(_attributeType.GetType());
        }
    }
}