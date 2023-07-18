using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class CollectablesSystem : NetworkBehaviour
    {
        [SerializeField] private TriggerNotifier triggerNotifier;

        [SyncVar(hook = nameof(OnWealthChangeHook))]
        private int wealth;

        public int Wealth 
        { 
            get { return wealth; }
            [Server]
            private set 
            { 
                wealth = value; 
                if(isServerOnly)
                {
                    OnWealthChange?.Invoke(value);
                }
            }
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
                if(isServer)
                {
                    wealth += coin.Value;
                    //Debug.Log("Coins: "+ Wealth);
                }

                coin.Disappear();
            }
        }

        [Client]
        private void OnWealthChangeHook(int oldWealth, int newWealth)
        {
            OnWealthChange?.Invoke(newWealth);
        }
    }
}