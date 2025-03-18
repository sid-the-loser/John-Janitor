namespace Main.Scripts.Common
{
    public static class GlobalVariables
    {
        public static bool Paused = false;

        #region Enemy Stats
        
        public static float EnemyMaxHealth { get; set;}
        public static float EnemyBaseDamage { get; set; }
        public static float EnemyBaseHpRegen { get; set; }
        public static float EnemyAttSpeed { get; set; }
        public static float EnemyMoveSpeed = 3.0f; // TODO:setup get set please
        public static float EnemyAttRange { get; set; }
        public static float EnemyBaseDefense { get; set; }
        public static float EnemyDodgeChance { get; set; }
        public static float EnemyCritChance { get; set; }
        public static float EnemyCritDamage { get; set; }
        public static string EnemyElement { get; set; } // dont touch for now
        
        #endregion

        #region Player Stats
        
        public static float PlayerMaxHealth { get; set; }
        public static float PlayerBaseDamage { get; set; }
        public static float PlayerBaseHpRegen { get; set; }
        public static float PlayerAttSpeed { get; set; }
        public static float PlayerMoveSpeed { get; set; }
        public static float PlayerAttRange { get; set; }
        public static float PlayerBaseDefense { get; set; }
        public static float PlayerDodgeChance { get; set; }
        public static float PlayerCritChance { get; set; }
        public static float PlayerCritDamage { get; set; }
        public static string PlayerElement { get; set; } // dont touch for now
        
        #endregion

        #region Level Transition Related

        public static int NextLevelIndex = 1;

        #endregion
    }
}