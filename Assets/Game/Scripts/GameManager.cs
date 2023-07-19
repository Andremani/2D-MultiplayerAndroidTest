using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Andremani.TwoDMultiplayerAndroidTest.Utility;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class GameManager : NetworkSingleton<GameManager>
    {
        //Server use only
        private readonly Dictionary<NetworkConnectionToClient, Player> playerConnections = new Dictionary<NetworkConnectionToClient, Player>();
        //public Dictionary<NetworkConnectionToClient, Player> Players { [Server] get { return playerConnections; } }

        public readonly SyncList<Player> alivePlayers = new SyncList<Player>();

        public static event Action<Player> OnGameOver;

        [ServerCallback]
        protected override void Awake()
        {
            base.Awake();

            if(NetworkManager.singleton is NetRoomManager netRoomManager)
            {
                netRoomManager.OnServerAddedGamePlayer += OnPlayerAdded;
                netRoomManager.OnServerDisconnectedPlayer += OnPlayerDisconnected;
                netRoomManager.OnLeaveMainGameScene += ClearPlayers;
            }
            else
            {
                Debug.LogError("No NetRoomManager? Wrong NetworkManager type");
            }
        }

        [Server]
        private void OnPlayerAdded(NetworkConnectionToClient conn, Player newPlayer)
        {
            playerConnections.Add(conn, newPlayer);

            alivePlayers.Add(newPlayer);
            newPlayer.HealthSystem.OnDeath += OnPlayerDeath;
        }

        [Server]
        private void OnPlayerDisconnected(NetworkConnectionToClient conn)
        {
            if (playerConnections.ContainsKey(conn))
            {
                if(alivePlayers.Contains(playerConnections[conn]))
                {
                    alivePlayers.Remove(playerConnections[conn]);
                }
                playerConnections[conn].HealthSystem.OnDeath -= OnPlayerDeath;

                playerConnections.Remove(conn);
            }
        }

        [Server]
        private void OnPlayerDeath(Player deadPlayer)
        {
            alivePlayers.Remove(deadPlayer);
            if (alivePlayers.Count == 1)
            {
                GameOver(alivePlayers[0]);
            }
        }

        [Server]
        private void GameOver(Player winner)
        {
            OnGameOver?.Invoke(winner);

            RpcGameOver(winner);

            [ClientRpc]
            void RpcGameOver(Player winner)
            {
                if(isClientOnly)
                {
                    OnGameOver?.Invoke(winner);
                }
            }
        }

        [Server]
        private void ClearPlayers()
        {
            playerConnections.Clear();
            alivePlayers.Clear();
        }
    }
}