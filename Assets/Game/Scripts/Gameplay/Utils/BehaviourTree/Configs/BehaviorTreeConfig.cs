using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Configs
{
    [CreateAssetMenu(fileName = "NodeConfig", menuName = "Game/Configs/Entity/Behaviour/NodeConfig")]
    public sealed class BehaviorTreeConfig : ScriptableObject
    {
        [SerializeReference, SubclassSelector] private INode _node;
        
        public INode Create()
        {
            return _node.CloneNode();
        }
    }
}