using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class HealthSystem : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Player player;
        [Header("Options")]
        [SerializeField] private float maxHealth = 100;
        [SyncVar(hook = nameof(OnHealthChangeHook))]
        [SerializeField] private float currentHealth = 100;

        public float MaxHealth { get { return maxHealth; } }
        public float Health 
        { 
            get { return currentHealth; }

            [Server]
            private set 
            { 
                if(value <= 0)
                {
                    currentHealth = 0;
                    Death();
                }
                else
                {
                    currentHealth = value;
                }

                if(isServerOnly)
                {
                    OnHealthChange?.Invoke(value);
                }
            }
        }

        public event Action<float> OnHealthChange;

        [Server]
        public void RecieveDamage(float damage)
        {
            Health -= damage;
        }

        [ServerCallback]
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                RecieveDamage(30);
            }
        }

        [Client]
        private void OnHealthChangeHook(float oldHealth, float newHealth)
        {
            OnHealthChange?.Invoke(newHealth);
        }

        [Server]
        private void Death()
        {
            //check of game over conditions
            RpcOnDeath();
        }

        [ClientRpc]
        private void RpcOnDeath()
        {
            player.gameObject.SetActive(false);
        }
    }
}