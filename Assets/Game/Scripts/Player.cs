using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Andremani.TwoDMultiplayerAndroidTest.PlayerSystems;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class Player : NetworkBehaviour
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] public PlayerMovementController MovementController { get; private set; }
        [field: SerializeField] public PlayerOrientation PlayerOrientation { get; private set; }
        [field: SerializeField] public ShootingSystem ShootingSystem { get; private set; }
        [field: SerializeField] public CollectablesSystem CollectablesSystem { get; private set; }
        [field: SerializeField] public HealthSystem HealthSystem { get; private set; }

        [SyncVar(hook = nameof(HookOnNicknameChange))]
        private string nickname;
        public string Nickname 
        { 
            get { return nickname; } 
            [Server] set { nickname = value; } 
        }

        public event Action<string> OnNicknameChange;

        void Start()
        {
            MovementController.Init(PlayerInput);
            PlayerOrientation.Init(PlayerInput);
            ShootingSystem.Init(PlayerInput, this);
        }

        [Client]
        private void HookOnNicknameChange(string oldNickname, string newNickname)
        {
            OnNicknameChange?.Invoke(newNickname);
        }
    }
}