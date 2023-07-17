using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class CollectablesSystem : MonoBehaviour
    {
        [SerializeField] private TriggerNotifier triggerNotifier;

        private int wealth;

        public int Wealth 
        { 
            get { return wealth; } 
            set { wealth = value; OnWealthChanged?.Invoke(value); } 
        }

        public event Action<int> OnWealthChanged;

        void Start()
        {
            triggerNotifier.OnTriggerEnterEvent += OnTriggerEntering;
        }

        private void OnTriggerEntering(Collider2D otherCollider)
        {
            if(otherCollider.TryGetComponent<Coin>(out Coin coin))
            {
                Wealth += coin.Value;
                coin.Disappear();

                //Debug.Log("Coins: "+ Wealth);
            }
        }
    }
}