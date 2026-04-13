namespace TowerDefence.Gameplay.Entity.Domain
{
    public enum EAnimationState
    {
        Empty,
        
        Idle,
        
        Moving,
        
        WindUp,
        Attacking,
        
        Stunning,
        Death,
    }
    
    public static class AnimationKeys
    {
        public const string CancelTrigger = "Cancel";
        public const string CancelAllTrigger = "CancelAll";
        
        public const string StunTrigger = "Stun";
        public const string DieTrigger = "Die";
        public const string IdleTrigger = "Idle";
        
        public const string SkillTrigger = "Skill";
        
        public const string WindUpTrigger = "WindUp";
        public const string AttackTrigger = "Attack";
        
        public const string MoveTrigger = "Move";
        
        public const string IsAttackFlag = "IsAttack";
        public const string IsMoveFlag = "IsMove";
        
        public const int BottomLayer = 0;
        public const int TopLayer = 1;
        public const int FullBodyLayer = 2;


        public const string AttackSpeedFloat = "AttackSpeed";
        public const string SkillSpeedFloat = "SkillSpeed";
    }
}