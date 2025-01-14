namespace Sid.Scripts.Common
{
    public static class GlobalVariables
    {
        public static bool Paused = false;
        public static float MaxHealth { get; set;}
        public static float BaseDamage { get; set; }
        public static float BaseHpRegen { get; set; }
        public static float AttSpeed { get; set; }
        public static float MoveSpeed = 3.0f; // TODO:setup get set please
        public static float AttRange { get; set; }
        public static float BaseDefense { get; set; }
        public static float DodgeChance { get; set; }
        public static float CritChance { get; set; }
        public static float CritDamage { get; set; }
        public static string Element { get; set; } // dont touch for now
    }
}