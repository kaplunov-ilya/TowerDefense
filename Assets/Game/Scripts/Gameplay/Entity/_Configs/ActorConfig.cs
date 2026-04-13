using System.Collections.Generic;
using TowerDefence.Gameplay.Behaviour.Configs;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Stats.Configs;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Configs
{
    [CreateAssetMenu(fileName = "ActorConfig", menuName = "Game/Configs/Entity/Actor/ActorConfig")]
    public sealed class ActorConfig : ScriptableObject
    {
        [field: SerializeField] public ActorView ActorViewPrefab { get; private set; }
        [field: SerializeField] public ActorBehaviourTreeConfig  ActorBehaviourTreeConfig { get; private set; }
        [field: SerializeReference] public List<BehaviourConfig>  BehavioursConfigs { get; private set; }
        [field: SerializeReference] public AttributesConfig  AttributesConfigs { get; private set; }
        [field: SerializeReference] public ResourcesConfig  ResourceConfigs { get; private set; }
        
        //[field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; }
        [field: SerializeField] public EEntityTraits Traits { get; private set; }
        [field: SerializeField] public EEntityTraits[] EnemyPriority { get; private set; }
    }
}