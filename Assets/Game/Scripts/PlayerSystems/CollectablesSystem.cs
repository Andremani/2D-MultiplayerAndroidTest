using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class CollectablesSystem : MonoBehaviour
    {
        [SerializeField] private TriggerNotifier triggerNotifier;

        private int wealth;

        public int Wealth 
        { 
            get { return wealth; } 
            set { wealth = value; OnWealthChange?.Invoke(value); } 
        }

        public event Action<int> OnWealthChange;

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