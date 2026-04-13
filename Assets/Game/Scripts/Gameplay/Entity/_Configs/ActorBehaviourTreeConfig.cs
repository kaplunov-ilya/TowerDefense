using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Configs
{
    [CreateAssetMenu(fileName = "BehaviourTreeConfig", menuName = "Game/Configs/Entity/Actor/BehaviourTreeConfig")]
    public sealed class ActorBehaviourTreeConfig : ScriptableObject
    {
        [SerializeReference, SubclassSelector] private INode _node;
        
        public INode Create()
        {
            return _node.CloneNode();
        }
    }
}