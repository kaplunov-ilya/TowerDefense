using System;

namespace TowerDefence.Gameplay.Entity.Configs.Meta.Contract
{
    /// <summary>
    /// Какие-то данные присущие для конкретных задач
    /// </summary>
    public interface IMetaConfig
    {
        Type MetaType { get; } 
    }
    
    /// <summary>
    /// Какие-то данные присущие для конкретных задач
    /// </summary>
    public interface IMeta
    {
        Type MetaType { get; } 
    }
}