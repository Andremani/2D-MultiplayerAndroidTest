using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerOrientation playerOrientation;
        [SerializeField] private ShootingSystem shootingSystem;
        [SerializeField] private CollectablesSystem collectablesSystem;

        void Start()
        {
            movementController.Init(playerInput);
            playerOrientation.Init(playerInput);
            shootingSystem.Init(playerInput, this);
        }
    }
}