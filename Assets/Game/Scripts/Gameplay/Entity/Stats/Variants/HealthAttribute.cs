using System;

namespace TowerDefence.Gameplay.Entity.Stats.Variants
{
    [Serializable]
    public sealed class HealthAttribute : Attribute {}
    [Serializable]
    public sealed class ManaAttribute : Attribute {}
    [Serializable]
    public sealed class ResistAttribute : Attribute {}
    [Serializable]
    public sealed class SpeedMoveAttribute : Attribute { }
    [Serializable]
    public sealed class DamageAttribute : Attribute { }
}