using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Common;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    /*
    Add a corresponding number for each weapon
     0 - Melee (no weapon)
     1 - ToiletBrush

     */
    
    public static void UpgradeStats(int WeaponNumber)
    {
        if (Input.GetMouseButton(0))
        {
            switch (WeaponNumber)
            {
                case 0:
                    GlobalVariables.PlayerBaseDamage += 0;
                    GlobalVariables.PlayerAttSpeed += 0;
                    GlobalVariables.PlayerAttRange += 0;
                    GlobalVariables.PlayerCritChance += 0;
                    GlobalVariables.PlayerCritDamage += 0;
                    break;
                case 1:
                    GlobalVariables.PlayerBaseDamage += 5;
                    GlobalVariables.PlayerAttSpeed += 0.25f;
                    GlobalVariables.PlayerAttRange += 0.5f;
                    GlobalVariables.PlayerCritChance += 0.05f;
                    GlobalVariables.PlayerCritDamage += 1.5f;
                    break;
            }
        }
    }
    
    public static void ResetStats(int WeaponNumber)
    {
        switch (WeaponNumber)
        {
            case 0:
                GlobalVariables.PlayerBaseDamage -= 0;
                GlobalVariables.PlayerAttSpeed -= 0;
                GlobalVariables.PlayerAttRange -= 0;
                GlobalVariables.PlayerCritChance -= 0;
                GlobalVariables.PlayerCritDamage -= 0;
                break;
            case 1:
                GlobalVariables.PlayerBaseDamage -= 5;
                GlobalVariables.PlayerAttSpeed -= 0.25f;
                GlobalVariables.PlayerAttRange -= 0.5f;
                GlobalVariables.PlayerCritChance -= 0.05f;
                GlobalVariables.PlayerCritDamage -= 1.5f;
                break;
        }
    }
}
