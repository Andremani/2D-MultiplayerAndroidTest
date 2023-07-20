using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andremani.TwoDMultiplayerAndroidTest.UI;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick leftJoystick;
        [SerializeField] private Joystick rightJoystick;

        public Vector2 MovementInput { get; private set; }
        public Vector2 ViewDirection { get; private set; }
        public Vector2 ShootingDirection { get; private set; }

        private void Start()
        {
            leftJoystick = UIManager.I.LeftJoystick;
            rightJoystick = UIManager.I.RightJoystick;
        }

        private void Update()
        {
            ScanMovementInput();
            ViewDirection = rightJoystick.Direction;
            ShootingDirection = rightJoystick.Direction;
        }

        private void ScanMovementInput()
        {
            if(leftJoystick.Direction != Vector2.zero)
            {
                MovementInput = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical);
            }
            else
            {
                MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }
    }
}