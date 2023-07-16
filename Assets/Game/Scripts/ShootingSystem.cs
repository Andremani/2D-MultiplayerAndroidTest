using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class ShootingSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject owner;
        [SerializeField] private Transform projectileSpawningPoint;
        [SerializeField] private Transform projectilesParent;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Joystick shootingJoystick;
        [Header("Options")]
        [SerializeField] private float shootingCooldown = 0.5f;
        private bool isShootingRoutineRunning;
        private bool canShoot = true;

        public bool CanShoot 
        { 
            get => canShoot; 
            set
            {
                canShoot = value;
                if(canShoot && !isShootingRoutineRunning)
                {
                    StartCoroutine(ShootingRoutine());
                }
            }
        }

        private void Start()
        {
            StartCoroutine(ShootingRoutine());
        }

        private IEnumerator ShootingRoutine()
        {
            isShootingRoutineRunning = true;
            while (CanShoot)
            {
                yield return new WaitUntil(() => shootingJoystick.Direction != Vector2.zero);
                if (CanShoot)
                {
                    Shoot();
                }
                yield return new WaitForSeconds(shootingCooldown);
            }
            isShootingRoutineRunning = false;
        }

        private void Shoot()
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawningPoint.position, projectileSpawningPoint.rotation, projectilesParent);
            projectile.Owner = owner;
        }
    }
}