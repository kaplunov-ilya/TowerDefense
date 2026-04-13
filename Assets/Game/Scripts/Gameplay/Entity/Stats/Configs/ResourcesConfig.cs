using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Stats.Configs.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Stats.Configs
{
    [CreateAssetMenu(fileName = "ResourcesConfig", menuName = "Game/Configs/Entity/Actor/ResourcesConfig")]
    public sealed class ResourcesConfig : ScriptableObject, IResourcesConfig
    {
        [SerializeReference, SubclassSelector] private List<IResourceConfig> _resourcesType;
        
        public IResource[] Create()
        {
            var resources = new IResource[_resourcesType.Count];;
            
            for (var index = 0; index < _resourcesType.Count; index++)
            {
                var resourceConfig = _resourcesType[index];
                var resource = resourceConfig.Create();
                resources[index] = resource;
            }

            return resources;
        }
    }
}