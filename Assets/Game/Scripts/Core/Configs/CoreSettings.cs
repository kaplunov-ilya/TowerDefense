using Core.Logger.Domain;
using UnityEngine;

namespace TowerDefence.Core.Configs
{
    [CreateAssetMenu(fileName = "CoreSettings", menuName = "Game/Configs/Core/Core Settings")]
    public sealed class CoreSettings : ScriptableObject
    {
        public ELogLevels ProjectLogLevel { get; set; }
    }
}