using System;
using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Stats.Configs.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Stats.Configs
{
    [CreateAssetMenu(fileName = "AttributesConfig", menuName = "Game/Configs/Entity/Actor/AttributesConfig")]
    public sealed class AttributesConfig : ScriptableObject, IAttributesConfig
    {
        [SerializeReference, SubclassSelector] private List<IAttributeConfig> _attributesType;

        public IAttribute[] Create()
        {
            IAttribute[] attributes = new IAttribute[_attributesType.Count];

            for (var index = 0; index < _attributesType.Count; index++)
            {
                var attributeConfig = _attributesType[index];
                var attribute = attributeConfig.Create();
                attributes[index] = attribute;
            }

            return attributes;
        }
    }
    
    [Serializable]
    public sealed class AttributeConfig : IAttributeConfig
    {
        [SerializeReference, SubclassSelector] private IAttribute _attributesType;
        [SerializeField] private float _value;

        public IAttribute Create()
        {
            if (_attributesType == null)
                throw new System.Exception("StatType not assigned!");

            var stat = (IAttribute)Activator.CreateInstance(_attributesType.GetType());
            stat.SetBaseValue(_value);
            
            return stat;
        }
    }
}