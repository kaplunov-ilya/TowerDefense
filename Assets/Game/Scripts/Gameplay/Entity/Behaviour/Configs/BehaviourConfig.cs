using TowerDefence.Gameplay.Behaviour.Configs.Contracts;
using TowerDefence.Gameplay.Behaviour.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour.Configs
{
    [CreateAssetMenu(fileName = "BehaviourConfig", menuName = "Game/Configs/Entity/Actor/BehaviourConfig")]
    public sealed class BehaviourConfig : ScriptableObject, IBehaviourConfig
    {
        [SerializeReference, SubclassSelector] private IBehaviour _behaviourType;
        
        public IBehaviour Create()
        {
            var behaviour = _behaviourType.CloneBehaviour();
            
            return behaviour;
        }
    }
}