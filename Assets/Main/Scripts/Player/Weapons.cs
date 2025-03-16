using System.Collections;
using Main.Scripts.Common;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class Weapons : MonoBehaviour
    {
        private static bool isExtinguisherHeld = false;
        private static bool isSprayBottleHeld = false;
        private static bool isStaplerHeld = false;

        /*
        Corresponding number for each weapon
        0 - Melee (no weapon)
        1 - ToiletBrush
        2 - Plunger
        3 - FeatherDuster
        4 - Broom
        5 - Mop
        6 - WetFloorSign
        7 - FireExtinguisher (Ranged)
        8 - CleaningSprayBottles (Ranged)
        9 - CleaningGloves
        10 - Stapler  (Ranged)
        11 - OfficePhone
        12 - Ladder
        */

        public static void UpgradeStats(int weaponNumber)
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
                case 7: // Fire Extinguisher
                    isExtinguisherHeld = true;
                    break;
                case 8: // Cleaning Spray Bottles
                    isSprayBottleHeld = true;
                    break; 
                case 9: // Cleaning Gloves
                    GlobalVariables.PlayerBaseDamage += 5;
                    GlobalVariables.PlayerAttSpeed += 75;
                    GlobalVariables.PlayerAttRange += 1;
                    GlobalVariables.PlayerCritChance += 30;
                    GlobalVariables.PlayerCritDamage += 10;
                    break;
                case 10: // Stapler
                    isStaplerHeld = true;
                    break;
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
                case 7: // Fire Extinguisher
                    isExtinguisherHeld = false;
                    break; 
                case 8: // Cleaning Spray Bottles
                    isSprayBottleHeld = false; 
                    break; 
                case 9: // Cleaning Gloves
                    GlobalVariables.PlayerBaseDamage -= 5;
                    GlobalVariables.PlayerAttSpeed -= 75;
                    GlobalVariables.PlayerAttRange -= 1;
                    GlobalVariables.PlayerCritChance -= 30;
                    GlobalVariables.PlayerCritDamage -= 10;
                    break; 
                case 10: // Stapler
                    isStaplerHeld = false;
                    break;
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

        [SerializeField] private GameObject extinguisherBulletPrefab;
        [SerializeField] private GameObject sprayBottleBulletPrefab;
        [SerializeField] private GameObject stapleBulletPrefab;
        [SerializeField] private Transform firePoint;

        private float extinguisherCooldown = 0.1f;
        private float extinguisherCooldownTimer = 0f;

        private float sprayBottleCooldown = 0.25f;
        private int sprayPellets = 6;
        private float sprayCooldownTimer = 0f;

        private float staplerCooldown = 0.5f;
        private float staplerCooldownTimer = 0f;

        void Update()
        {
            if (Input.GetMouseButton(0) && extinguisherCooldownTimer <= 0 && isExtinguisherHeld)
            {
                FireProjectile(extinguisherBulletPrefab, 50f);
                extinguisherCooldownTimer = extinguisherCooldown;
            }

            if (Input.GetMouseButton(0) && sprayCooldownTimer <= 0 && isSprayBottleHeld)
            {
                for (int i = 0; i < sprayPellets; i++)
                {
                    Vector3 randomOffset = new Vector3(
                        0,
                        Random.Range(-0.5f, 0.5f),
                        Random.Range(-0.5f, 0.5f)
                    );
                    
                    Vector3 spawnPosition = firePoint.position + randomOffset;
                    
                    FireProjectile(sprayBottleBulletPrefab, 40f, spawnPosition);
                }
                sprayCooldownTimer = sprayBottleCooldown;
            }
            
            if (Input.GetMouseButton(0) && staplerCooldownTimer <= 0 && isStaplerHeld)
            {
                FireProjectile(stapleBulletPrefab, 60f);
                staplerCooldownTimer = staplerCooldown;
            }

            if (extinguisherCooldownTimer > 0) extinguisherCooldownTimer -= Time.deltaTime;
            if (sprayCooldownTimer > 0) sprayCooldownTimer -= Time.deltaTime;
            if (staplerCooldownTimer > 0) staplerCooldownTimer -= Time.deltaTime;
        }

        void FireProjectile(GameObject projectilePrefab, float speed, Vector3? overridePosition = null)
        {
            if (projectilePrefab)
            {
                Vector3 spawnPosition = overridePosition ?? firePoint.position;
                GameObject projectile = Instantiate(projectilePrefab, spawnPosition, firePoint.rotation);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity = firePoint.forward * speed;
                }

                projectile.AddComponent<ProjectileCollisionHandler>();
                
                Destroy(projectile, 2f); // Auto destroy after 2 seconds
            }
        }
    }
}