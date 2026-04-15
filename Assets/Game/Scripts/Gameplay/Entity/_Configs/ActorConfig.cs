using System.Collections.Generic;
using TowerDefence.Core.Utils.Storage;
using TowerDefence.Gameplay.Behaviour.Configs;
using TowerDefence.Gameplay.Entity.Configs.Meta.Contract;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Stats.Configs;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Configs
{
    [CreateAssetMenu(fileName = "ActorConfig", menuName = "Game/Configs/Entity/Actor/ActorConfig")]
    public sealed class ActorConfig : ScriptableObject
    {
        [SerializeReference, SubclassSelector] private List<IMetaConfig> _meta;
        
        [field: SerializeField] public ActorView ActorViewPrefab { get; private set; }
        [field: SerializeField] public ActorBehaviourTreeConfig  ActorBehaviourTreeConfig { get; private set; }
        [field: SerializeReference] public List<BehaviourConfig>  BehavioursConfigs { get; private set; }
        [field: SerializeReference] public AttributesConfig  AttributesConfigs { get; private set; }
        [field: SerializeReference] public ResourcesConfig  ResourceConfigs { get; private set; }
        
        [field: SerializeField] public EEntityTraits Traits { get; private set; }
        [field: SerializeField] public EEntityTraits[] EnemyPriority { get; private set; }

        public GroupStorage<IMetaConfig> Meta { get; private set; } 

        public void Init()
        {
            Meta = new(_meta.Count);

            foreach (var meta in _meta)
            {
                Meta.Add(meta.MetaType, meta);
            }
        }
    }
}