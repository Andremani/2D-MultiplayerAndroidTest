using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class ShootingSystem : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform projectileSpawningPoint;
        [SerializeField] private Transform projectilesParent;
        [SerializeField] private Projectile projectilePrefab;
        [Header("Options")]
        [SerializeField] private float shootingCooldown = 0.5f;

        private GameObject owner;
        private PlayerInput input;
        private bool isShootingRoutineRunning;
        private bool canShoot;

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
        private void Awake()
        {
            canShoot = false;
        }

        [ClientCallback]
        public void Init(PlayerInput input, Player player)
        {
            if (!isLocalPlayer)
            { return; }

            owner = player.gameObject;

            this.input = input;
            canShoot = true;

            StartCoroutine(ShootingRoutine());
        }

        [Client]
        private IEnumerator ShootingRoutine()
        {
            isShootingRoutineRunning = true;
            while (CanShoot)
            {
                yield return new WaitUntil(() => input.ShootingDirection != Vector2.zero);
                if (CanShoot)
                {
                    CmdShoot();
                }
                yield return new WaitForSeconds(shootingCooldown);
            }
            isShootingRoutineRunning = false;
        }

        [Command]
        private void CmdShoot()
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawningPoint.position, projectileSpawningPoint.rotation, projectilesParent);
            projectile.Owner = owner;

            NetworkServer.Spawn(projectile.gameObject);
        }
    }
}