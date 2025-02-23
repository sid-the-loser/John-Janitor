using Main.Scripts.Common;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    /*
    Corresponding number for each weapon
     0 - Melee (no weapon)
     1 - ToiletBrush
     2 - Plunger
     3 - FeatherDuster
     4 - Broom
     5 - Mop
     6 - WetFloorSign
     7 - FireExtinguisher
     8 - CleaningSprayBottles
     9 - CleaningGloves
     10 - Stapler
     11 - OfficePhone
     12 - Ladder

     */

    public static void UpgradeStats(int weaponNumber)
    {
        if (Input.GetMouseButton(0))
        {
            switch (weaponNumber) // commented out weapons are ranged weapons, that system is not set up
            {
                case 0: // Melee
                    GlobalVariables.PlayerBaseDamage += 0;
                    GlobalVariables.PlayerAttSpeed += 0;
                    GlobalVariables.PlayerAttRange += 0;
                    GlobalVariables.PlayerCritChance += 0;
                    GlobalVariables.PlayerCritDamage += 0;
                    break;
                case 1: // Toilet Brush
                    GlobalVariables.PlayerBaseDamage += 10;
                    GlobalVariables.PlayerAttSpeed += 30;
                    GlobalVariables.PlayerAttRange += 2;
                    GlobalVariables.PlayerCritChance += 8;
                    GlobalVariables.PlayerCritDamage += 25;
                    break;
                case 2: // Plunger
                    GlobalVariables.PlayerBaseDamage += 15;
                    GlobalVariables.PlayerAttSpeed += 25;
                    GlobalVariables.PlayerAttRange += 2.5f;
                    GlobalVariables.PlayerCritChance += 6;
                    GlobalVariables.PlayerCritDamage += 30;
                    break;
                case 3: // Feather Duster
                    GlobalVariables.PlayerBaseDamage += 8;
                    GlobalVariables.PlayerAttSpeed += 50;
                    GlobalVariables.PlayerAttRange += 3;
                    GlobalVariables.PlayerCritChance += 15;
                    GlobalVariables.PlayerCritDamage += 18;
                    break;
                case 4: // Broom
                    GlobalVariables.PlayerBaseDamage += 18;
                    GlobalVariables.PlayerAttSpeed += 20;
                    GlobalVariables.PlayerAttRange += 9;
                    GlobalVariables.PlayerCritChance += 10;
                    GlobalVariables.PlayerCritDamage += 35;
                    break;
                case 5: // Mop
                    GlobalVariables.PlayerBaseDamage += 20;
                    GlobalVariables.PlayerAttSpeed += 15;
                    GlobalVariables.PlayerAttRange += 8;
                    GlobalVariables.PlayerCritChance += 8;
                    GlobalVariables.PlayerCritDamage += 30;
                    break;
                case 6: // Wet Floor Sign
                    GlobalVariables.PlayerBaseDamage += 30;
                    GlobalVariables.PlayerAttSpeed += -2;
                    GlobalVariables.PlayerAttRange += 1.5f;
                    GlobalVariables.PlayerCritChance += -5;
                    GlobalVariables.PlayerCritDamage += 50;
                    break;
                /*
                 case 7: // Fire Extinguisher
                    GlobalVariables.PlayerBaseDamage += ??;
                    GlobalVariables.PlayerAttSpeed += ??;
                    GlobalVariables.PlayerAttRange += ??;
                    GlobalVariables.PlayerCritChance += ??;
                    GlobalVariables.PlayerCritDamage += ??;
                    break;
                case 8: // Cleaning Spray Bottles 
                    GlobalVariables.PlayerBaseDamage += ??;
                    GlobalVariables.PlayerAttSpeed += ??;
                    GlobalVariables.PlayerAttRange += ??;
                    GlobalVariables.PlayerCritChance += ??;
                    GlobalVariables.PlayerCritDamage += ??;
                    break;
                */
                case 9: // Cleaning Gloves
                    GlobalVariables.PlayerBaseDamage += 5;
                    GlobalVariables.PlayerAttSpeed += 75;
                    GlobalVariables.PlayerAttRange += 1;
                    GlobalVariables.PlayerCritChance += 30;
                    GlobalVariables.PlayerCritDamage += 10;
                    break;
                /*
                 case 10: // Stapler
                    GlobalVariables.PlayerBaseDamage += ??;
                    GlobalVariables.PlayerAttSpeed += ??;
                    GlobalVariables.PlayerAttRange += ??;
                    GlobalVariables.PlayerCritChance += ??;
                    GlobalVariables.PlayerCritDamage += ??;
                    break;
                */
                case 11: // Office Phone
                    GlobalVariables.PlayerBaseDamage += 22;
                    GlobalVariables.PlayerAttSpeed += 10;
                    GlobalVariables.PlayerAttRange += 4;
                    GlobalVariables.PlayerCritChance += 0;
                    GlobalVariables.PlayerCritDamage += 40;
                    break;
                case 12: // Ladder
                    GlobalVariables.PlayerBaseDamage += 35;
                    GlobalVariables.PlayerAttSpeed += -5;
                    GlobalVariables.PlayerAttRange += 10;
                    GlobalVariables.PlayerCritChance += -10;
                    GlobalVariables.PlayerCritDamage += 60;
                    break;
            }
        }
    }

    public static void ResetStats(int weaponNumber)
    {
        switch (weaponNumber) // commented out weapons are ranged weapons, that system is not set up
        {
            case 0: // Melee
                GlobalVariables.PlayerBaseDamage -= 0;
                GlobalVariables.PlayerAttSpeed -= 0;
                GlobalVariables.PlayerAttRange -= 0;
                GlobalVariables.PlayerCritChance -= 0;
                GlobalVariables.PlayerCritDamage -= 0;
                break;
            case 1: // Toilet Brush
                GlobalVariables.PlayerBaseDamage -= 10;
                GlobalVariables.PlayerAttSpeed -= 30;
                GlobalVariables.PlayerAttRange -= 2;
                GlobalVariables.PlayerCritChance -= 8;
                GlobalVariables.PlayerCritDamage -= 25;
                break;
            case 2: // Plunger
                GlobalVariables.PlayerBaseDamage -= 15;
                GlobalVariables.PlayerAttSpeed -= 25;
                GlobalVariables.PlayerAttRange -= 2.5f;
                GlobalVariables.PlayerCritChance -= 6;
                GlobalVariables.PlayerCritDamage -= 30;
                break;
            case 3: // Feather Duster
                GlobalVariables.PlayerBaseDamage -= 8;
                GlobalVariables.PlayerAttSpeed -= 50;
                GlobalVariables.PlayerAttRange -= 3;
                GlobalVariables.PlayerCritChance -= 15;
                GlobalVariables.PlayerCritDamage -= 18;
                break;
            case 4: // Broom
                GlobalVariables.PlayerBaseDamage -= 18;
                GlobalVariables.PlayerAttSpeed -= 20;
                GlobalVariables.PlayerAttRange -= 9;
                GlobalVariables.PlayerCritChance -= 10;
                GlobalVariables.PlayerCritDamage -= 35;
                break;
            case 5: // Mop
                GlobalVariables.PlayerBaseDamage -= 20;
                GlobalVariables.PlayerAttSpeed -= 15;
                GlobalVariables.PlayerAttRange -= 8;
                GlobalVariables.PlayerCritChance -= 8;
                GlobalVariables.PlayerCritDamage -= 30;
                break;
            case 6: // Wet Floor Sign
                GlobalVariables.PlayerBaseDamage -= 30;
                GlobalVariables.PlayerAttSpeed -= -2;
                GlobalVariables.PlayerAttRange -= 1.5f;
                GlobalVariables.PlayerCritChance -= -5;
                GlobalVariables.PlayerCritDamage -= 50;
                break;
            /*
             case 7: // Fire Extinguisher
                GlobalVariables.PlayerBaseDamage -= ??;
                GlobalVariables.PlayerAttSpeed -= ??;
                GlobalVariables.PlayerAttRange -= ??;
                GlobalVariables.PlayerCritChance -= ??;
                GlobalVariables.PlayerCritDamage -= ??;
                break;
            case 8: // Cleaning Spray Bottles
                GlobalVariables.PlayerBaseDamage -= ??;
                GlobalVariables.PlayerAttSpeed -= ??;
                GlobalVariables.PlayerAttRange -= ??;
                GlobalVariables.PlayerCritChance -= ??;
                GlobalVariables.PlayerCritDamage -= ??;
                break;
            */
            case 9: // Cleaning Gloves
                GlobalVariables.PlayerBaseDamage -= 5;
                GlobalVariables.PlayerAttSpeed -= 75;
                GlobalVariables.PlayerAttRange -= 1;
                GlobalVariables.PlayerCritChance -= 30;
                GlobalVariables.PlayerCritDamage -= 10;
                break;
            /*
             case 10: // Stapler
                GlobalVariables.PlayerBaseDamage -= ??;
                GlobalVariables.PlayerAttSpeed += ??;
                GlobalVariables.PlayerAttRange -= ??;
                GlobalVariables.PlayerCritChance -= ??;
                GlobalVariables.PlayerCritDamage -= ??;
                break;
            */
            case 11: // Office Phone
                GlobalVariables.PlayerBaseDamage -= 22;
                GlobalVariables.PlayerAttSpeed -= 10;
                GlobalVariables.PlayerAttRange -= 4;
                GlobalVariables.PlayerCritChance -= 0;
                GlobalVariables.PlayerCritDamage -= 40;
                break;
            case 12: // Ladder
                GlobalVariables.PlayerBaseDamage -= 35;
                GlobalVariables.PlayerAttSpeed -= -5;
                GlobalVariables.PlayerAttRange -= 10;
                GlobalVariables.PlayerCritChance -= -10;
                GlobalVariables.PlayerCritDamage -= 60;
                break;
        }
    }
}