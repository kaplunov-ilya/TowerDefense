using System;
using TowerDefence.Gameplay.Entity;

/// <summary> Возможность сущность, такие как ходьба, атака и тд </summary>
namespace TowerDefence.Gameplay.Behaviour.Contract
{
    /// <summary> Основной интерфейс Поведения актера </summary>
    public interface IBehaviour
    {
        Type Type { get; }
        
        Actor Owner { get; }

        void Init(Actor owner);
        
        /// <summary> Включить поведение </summary>
        void SetEnable();
        
        /// <summary> Выключить поведение </summary>
        void SetDisable();

        IBehaviour CloneBehaviour();
    }
}