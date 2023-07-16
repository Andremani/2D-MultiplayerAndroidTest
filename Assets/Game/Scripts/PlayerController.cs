using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andremani.TwoDMultiplayerAndroidTest.Utility;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private Joystick movementJoystick;

        void Update()
        {
            float horizontal = movementJoystick.Horizontal; //Input.GetAxis("Horizontal");
            float vertical = movementJoystick.Vertical; //Input.GetAxis("Vertical");

            Vector2 input = new Vector2(horizontal, vertical);
            Vector2 direction = input.normalized;

            Vector2 velocity = input.Abs() * direction * speed;

            //transform.Translate(velocity);

            rb.velocity = velocity;
        }
    }
}