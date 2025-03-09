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

        private float extinguisherFireRate = 0.1f;

        private float sprayBottleCooldown = 0.5f;
        private int sprayPellets = 6;
        private float spraySpreadAngle = 5f;
        private float sprayCooldownTimer = 0.35f;

        private float staplerCooldown = 0.5f;
        private float staplerCooldownTimer = 0.2f;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isExtinguisherHeld)
            {
                StartCoroutine(FireExtinguisher());
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopCoroutine(FireExtinguisher());
                isExtinguisherHeld = false;
            }
            
            if (Input.GetMouseButtonDown(0) && sprayCooldownTimer <= 0 && !isSprayBottleHeld)
            {
                FireSprayBottle();
                sprayCooldownTimer = sprayBottleCooldown;
            }
            
            if (Input.GetMouseButtonDown(0) && staplerCooldownTimer <= 0 && !isStaplerHeld)
            {
                FireStapler();
                staplerCooldownTimer = staplerCooldown;
            }
            
            if (sprayCooldownTimer > 0) sprayCooldownTimer -= Time.deltaTime;
            if (staplerCooldownTimer > 0) staplerCooldownTimer -= Time.deltaTime;
        }

        private IEnumerator FireExtinguisher()
        {
            isExtinguisherHeld = true;
            while (isExtinguisherHeld)
            {
                FireProjectile(extinguisherBulletPrefab, 15f);
                yield return new WaitForSeconds(extinguisherFireRate);
            }
        }

        void FireSprayBottle()
        {
            for (int i = 0; i < sprayPellets; i++)
            {
                float spread = Random.Range(-spraySpreadAngle, spraySpreadAngle);
                Quaternion pelletRotation = Quaternion.Euler(0, spread, 0) * firePoint.rotation;
                FireProjectile(sprayBottleBulletPrefab, 10f, pelletRotation);
            }
        }

        void FireStapler()
        {
            FireProjectile(stapleBulletPrefab, 20f);
        }

        void FireProjectile(GameObject projectilePrefab, float speed, Quaternion? overrideRotation = null)
        {
            if (projectilePrefab)
            {
                Quaternion spawnRotation = overrideRotation ?? firePoint.rotation;
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, spawnRotation);
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