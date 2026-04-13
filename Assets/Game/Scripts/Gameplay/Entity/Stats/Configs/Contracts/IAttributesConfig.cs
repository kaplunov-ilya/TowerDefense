using System.Collections.Generic;
using TowerDefence.Gameplay.Entity.Stats.Contracts;

namespace TowerDefence.Gameplay.Entity.Stats.Configs.Contracts
{
    public interface IAttributesConfig
    {
        IAttribute[] Create();
    }
    
    public interface IAttributeConfig
    {
        IAttribute Create();
    }
}