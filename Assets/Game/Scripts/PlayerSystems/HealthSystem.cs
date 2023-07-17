using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Player player;
        [Header("Options")]
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float currentHealth = 100;

        public float MaxHealth { get { return maxHealth; } }
        public float Health 
        { 
            get { return currentHealth; }
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
                OnHealthChange?.Invoke(value); 
            }
        }

        public event Action<float> OnHealthChange;

        public void RecieveDamage(float damage)
        {
            Health -= damage;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                RecieveDamage(30);
            }
        }

        private void Death()
        {
            player.gameObject.SetActive(false);
        }
    }
}