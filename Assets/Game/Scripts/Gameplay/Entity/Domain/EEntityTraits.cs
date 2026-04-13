using System;

namespace TowerDefence.Gameplay.Entity.Domain
{
    [Flags]
    public enum EEntityTraits
    {
        None = 0,

        Ground = 1 << 0, // 1
        Flying = 1 << 1, // 2
        Tower = 1 << 2,  // 4
        Boss = 1 << 3,   // 8
        Small = 1 << 4,  // 16
        Big = 1 << 5,  // 32
        /*Organic     = 1 << 4, // 16
        Boss        = 1 << 5, // 32
        Elite       = 1 << 6, // 64
        Summoned    = 1 << 7, // 128
        Hero        = 1 << 8, // 256
        Neutral     = 1 << 9  // 512*/
    }

    public enum EEntitySide
    {
        Enemy,
        Player,
    }
}