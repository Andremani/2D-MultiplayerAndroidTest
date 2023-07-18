using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class GameManager : NetworkBehaviour
    {
        public readonly SyncList<Player> players = new SyncList<Player>();
        public readonly SyncList<Player> alivePlayers = new SyncList<Player>();

        public event Action OnGameOver;

        [ServerCallback]
        private void Start()
        {
            ////get players from network manager?
            //NetworkManager.singleton
            foreach (Player player in players)
            {
                alivePlayers.Add(player);
                player.HealthSystem.OnDeath += OnPlayerDeath;
            }
        }

        private void OnPlayerDeath(Player deadPlayer)
        {
            alivePlayers.Remove(deadPlayer);
            if(alivePlayers.Count <= 0)
            {
                OnGameOver?.Invoke();
            }
        }
    }
}