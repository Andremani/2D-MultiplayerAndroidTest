using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Andremani.TwoDMultiplayerAndroidTest.Utility;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class PlayerMovementController : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;
        [Header("Options")]
        [SerializeField] private float speed;

        private PlayerInput input;

        private void Awake()
        {
            enabled = false;
        }

        [ClientCallback]
        public void Init(PlayerInput input)
        {
            if (!isLocalPlayer)
            { return; }

            this.input = input;
            enabled = true;
        }

        [ClientCallback]
        void Update()
        {
            if (!isLocalPlayer)
            { return; }

            Vector2 inputVector = input.MovementInput;
            Vector2 direction = inputVector.normalized;

            Vector2 velocity = inputVector.Abs() * direction * speed;

            rb.velocity = velocity;
        }
    }
}