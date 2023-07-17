using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Andremani.TwoDMultiplayerAndroidTest.PlayerSystems;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CollectablesSystem collectablesSystem;
        [SerializeField] HealthSystem healthSystem;
        [SerializeField] Image coinsBar;
        [SerializeField] Image healthBar;
        [Header("Options")]
        [SerializeField] private float coinsBarCapacity = 10f;

        void Start()
        {
            coinsBar.fillAmount = collectablesSystem.Wealth;
            collectablesSystem.OnWealthChange += UpdateCoinsBar;

            healthBar.fillAmount = healthSystem.Health / healthSystem.MaxHealth;
            healthSystem.OnHealthChange += UpdateHealthBar;
        }

        private void UpdateHealthBar(float newHealth)
        {
            float newFillAmount = newHealth / healthSystem.MaxHealth;
            healthBar.fillAmount = newFillAmount;
        }

        private void UpdateCoinsBar(int newWealthAmount)
        {
            float newFillAmount = newWealthAmount / coinsBarCapacity;
            newFillAmount = Mathf.Clamp01(newFillAmount);
            coinsBar.fillAmount = newFillAmount;
        }
    }
}