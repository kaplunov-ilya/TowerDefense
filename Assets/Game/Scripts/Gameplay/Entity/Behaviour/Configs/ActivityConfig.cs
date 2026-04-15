using TowerDefence.Gameplay.Behaviour.Configs.Contracts;
using TowerDefence.Gameplay.Behaviour.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour.Configs
{
    [CreateAssetMenu(fileName = "ActivityConfig", menuName = "Game/Configs/Entity/Actor/ActivityConfig")]
    public sealed class ActivityConfig : ScriptableObject, IActivityConfig
    {
        [SerializeReference, SubclassSelector] private IActivity _activityType;
        
        public IActivity Create()
        {
            var activity = _activityType.CloneActivity();
            
            return activity;
        }
    }
}