using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] CollectablesSystem collectablesSystem;
        [SerializeField] Image coinsBar;

        [SerializeField] private float coinsBarCapacity = 10f;

        void Start()
        {
            coinsBar.fillAmount = collectablesSystem.Wealth;
            collectablesSystem.OnWealthChanged += UpdateCoinsBar;
        }

        private void UpdateCoinsBar(int newWealthAmount)
        {
            float newFillAmount = newWealthAmount / coinsBarCapacity;
            newFillAmount = Mathf.Clamp01(newFillAmount);
            coinsBar.fillAmount = newFillAmount;
        }
    }
}