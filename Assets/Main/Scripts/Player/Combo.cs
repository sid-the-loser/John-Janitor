using Main.Scripts.Common;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class Combo : MonoBehaviour
    {
        [SerializeField] private float reset = 5f; //how long it takes for the combo to reset

        private static int combo; //current combo
        private static float timer; //time since last combo update
        
        #region Base Player Stats
        
        private float basePlayerMaxHealth;
        private float basePlayerBaseDamage;
        private float basePlayerBaseHpRegen;
        private float basePlayerAttSpeed;
        private float basePlayerMoveSpeed;
        private float basePlayerAttRange;
        private float basePlayerBaseDefense;
        private float basePlayerDodgeChance;
        private float basePlayerCritChance;
        private float basePlayerCritDamage;
        
        #endregion
    
        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
            combo = 0;
            GetBaseStats(); //gets base player stats
        }

        private void Update()
        {
            timer = timer + Time.deltaTime; //updates time since last combo update
            if (timer > reset) //when player runs out of combo timer
            {
                combo = 0; //resets combo
                timer = 0f; //resets timer
                Debug.Log(combo); //updates combo count
                ResetPlayerStats(); //resets player stats to default
                Debug.Log("Player Combo and Stats Boost Reset");
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void ComboIncrease()
        {
            combo++; //increments combo by 1 when player hits a Player
            timer = 0f; //resets timer
            Debug.Log(combo); //updates combo count
            if (combo % 5 == 0) //when combo is a multiple of 5
            {
                IncreasePlayerStats(); //increases the player stats
            }
        }

        private void GetBaseStats()
        {
            basePlayerMaxHealth  = GlobalVariables.PlayerMaxHealth;
            basePlayerBaseDamage  = GlobalVariables.PlayerBaseDamage;
            basePlayerBaseHpRegen  = GlobalVariables.PlayerBaseHpRegen;
            basePlayerAttSpeed  = GlobalVariables.PlayerAttSpeed;
            basePlayerMoveSpeed  = GlobalVariables.PlayerMoveSpeed;
            basePlayerAttRange  = GlobalVariables.PlayerAttRange;
            basePlayerBaseDefense  = GlobalVariables.PlayerBaseDefense;
            basePlayerDodgeChance  = GlobalVariables.PlayerDodgeChance;
            basePlayerCritChance  = GlobalVariables.PlayerCritChance;
            basePlayerCritDamage  = GlobalVariables.PlayerCritDamage;
        }
        private static void IncreasePlayerStats()
        {
            int option; //stores random option
            option = Random.Range(0, 10); //selects a random option
            
            switch (option) //updates player stats
            {
                case 0:
                    GlobalVariables.PlayerMaxHealth *= 1.05f;
                    Debug.Log("Increased player health");
                    break;
                case 1:
                    GlobalVariables.PlayerBaseDamage *= 1.05f;
                    Debug.Log("Increased player damage");
                    break;
                case 2:
                    GlobalVariables.PlayerBaseHpRegen *= 1.025f;
                    Debug.Log("Increased player health regeneration");
                    break;
                case 3:
                    GlobalVariables.PlayerAttSpeed *= 1.025f;
                    Debug.Log("Increased player attack speed");
                    break;
                case 4:
                    GlobalVariables.PlayerMoveSpeed *= 1.05f;
                    Debug.Log("Increased player movement speed");
                    break;
                case 5:
                    GlobalVariables.PlayerAttRange *= 1.075f;
                    Debug.Log("Increased player attack range");
                    break;
                case 6:
                    //Not Added Yet
                    Debug.Log("not added yet");
                    break;
                case 7:
                    GlobalVariables.PlayerBaseDefense *= 1.05f;
                    Debug.Log("Increased player defence");
                    break;
                case 8:
                    GlobalVariables.PlayerDodgeChance += (GlobalVariables.PlayerDodgeChance*0.025f);
                    Debug.Log("Increased player dodge chance");
                    break;
                case 9:
                    GlobalVariables.PlayerCritChance += (GlobalVariables.PlayerCritChance*0.025f);
                    Debug.Log("Increased player crit chance");
                    break;
                case 10:
                    GlobalVariables.PlayerCritDamage += (GlobalVariables.PlayerCritDamage*0.01f);
                    Debug.Log("Increased player crit damage");
                    break;
            }
        }

        private void ResetPlayerStats()
        {
            GlobalVariables.PlayerMaxHealth = basePlayerMaxHealth;
            GlobalVariables.PlayerBaseDamage = basePlayerBaseDamage;
            GlobalVariables.PlayerBaseHpRegen = basePlayerBaseHpRegen;
            GlobalVariables.PlayerAttSpeed = basePlayerAttSpeed;
            GlobalVariables.PlayerMoveSpeed = basePlayerMoveSpeed;
            GlobalVariables.PlayerAttRange = basePlayerAttRange;
            GlobalVariables.PlayerBaseDefense = basePlayerBaseDefense;
            GlobalVariables.PlayerDodgeChance = basePlayerDodgeChance;
            GlobalVariables.PlayerCritChance = basePlayerCritChance;
            GlobalVariables.PlayerCritDamage = basePlayerCritDamage;
        }
        
    }
}
