using System;

namespace TowerDefence.Gameplay.Entity.Providers.Contracts
{
    /// <summary> Позволяет обращаться к визуальной составляющей </summary>
    public interface IEntityProvider
    {
        /// <summary> Чтобы добавлять в словари по конкретному типу </summary>
        Type EntityProviderType { get; }
    }
}