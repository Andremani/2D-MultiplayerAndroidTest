using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andremani.TwoDMultiplayerAndroidTest.Utility;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class PlayerMovementController : MonoBehaviour
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

        public void Init(PlayerInput input)
        {
            this.input = input;
            enabled = true;
        }

        void Update()
        {
            Vector2 inputVector = input.MovementInput;
            Vector2 direction = inputVector.normalized;

            Vector2 velocity = inputVector.Abs() * direction * speed;

            rb.velocity = velocity;
        }
    }
}