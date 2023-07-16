using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class PlayerOrientation : MonoBehaviour
    {
        [SerializeField] private Transform playerVisuals;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private Transform weapon;
        [SerializeField] private SpriteRenderer weaponRenderer;
        [SerializeField] private Joystick playerRotationJoystick;

        private Vector2 lastDirection;

        private void Update()
        {
            if (playerRotationJoystick.Direction != Vector2.zero)
            {
                lastDirection = playerRotationJoystick.Direction;
            }

            FlipCheck();
            WeaponAimingControl();
            WeaponSpriteSorting();
        }

        private void FlipCheck()
        {
            if (lastDirection.x >= 0)
            {
                if (playerVisuals.localScale.x < 0)
                {
                    FlipPlayer();
                }
            }
            else
            {
                if (playerVisuals.localScale.x > 0)
                {
                    FlipPlayer();
                }
            }
        }

        private void FlipPlayer()
        {
            FlipByScaleX(playerVisuals);
            FlipPositionX(weaponHolder);
            //weaponRenderer.flipY = !weaponRenderer.flipY;

            Vector3 tempScale = weaponHolder.localScale;
            tempScale.y = -tempScale.y;
            weaponHolder.localScale = tempScale;
        }

        private void FlipByScaleX(Transform transformToFlip)
        {
            Vector3 tempScale = transformToFlip.localScale;
            tempScale.x = -tempScale.x;
            transformToFlip.localScale = tempScale;
        }

        private void FlipPositionX(Transform transformToFlip)
        {
            Vector3 tempPosition = transformToFlip.localPosition;
            tempPosition.x = -tempPosition.x;
            transformToFlip.localPosition = tempPosition;
        }

        private void WeaponAimingControl()
        {
            Vector2 weaponDirection = lastDirection;

            float angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;
            weaponHolder.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }

        private void WeaponSpriteSorting()
        {
            if(lastDirection.y > 0)
            {
                if(weaponRenderer.sortingOrder > -10)
                {
                    weaponRenderer.sortingOrder = -10;
                }
            }
            else
            {
                if(weaponRenderer.sortingOrder < 10)
                {
                    weaponRenderer.sortingOrder = 10;
                }
            }
        }
    }
}