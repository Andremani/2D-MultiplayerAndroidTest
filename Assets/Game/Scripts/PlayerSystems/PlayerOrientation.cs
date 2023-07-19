using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.PlayerSystems
{
    public class PlayerOrientation : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerVisuals;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private Transform weapon;
        [SerializeField] private SpriteRenderer weaponRenderer;
        [Header("Options")]
        [SerializeField] private int weaponSortingLayerAboveBody = 10;
        [SerializeField] private int weaponSortingLayerBehindBody = -10;

        private PlayerInput input;
        private Vector2 lastDirection;

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
        private void Update()
        {
            if (!isLocalPlayer) 
            { return; }

            if (input.ViewDirection != Vector2.zero)
            {
                lastDirection = input.ViewDirection;
            }

            FlipCheck();
            WeaponAimingControl();
            WeaponSpriteSorting();
        }

        [Client]
        private void FlipCheck()
        {
            if (lastDirection.x >= 0)
            {
                if (playerVisuals.localScale.x < 0)
                {
                    FlipPlayer();
                    CmdFlipPlayer();
                }
            }
            else
            {
                if (playerVisuals.localScale.x > 0)
                {
                    FlipPlayer();
                    CmdFlipPlayer();
                }
            }
        }

        [Client]
        private void FlipPlayer()
        {
            Vector3 tempScale = playerVisuals.localScale;
            tempScale.x = -tempScale.x;
            playerVisuals.localScale = tempScale;

            Vector3 tempPosition = weaponHolder.localPosition;
            tempPosition.x = -tempPosition.x;
            weaponHolder.localPosition = tempPosition;

            tempScale = weaponHolder.localScale;
            tempScale.y = -tempScale.y;
            weaponHolder.localScale = tempScale;
        }

        [Command]
        private void CmdFlipPlayer()
        {
            RpcFlipPlayer();

            [ClientRpc(includeOwner = false)]
            void RpcFlipPlayer()
            {
                FlipPlayer();
            }
        }

        [Client]
        private void WeaponAimingControl()
        {
            Vector2 weaponDirection = lastDirection;

            float angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;
            weaponHolder.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }

        [Client]
        private void WeaponSpriteSorting()
        {
            if(lastDirection.y > 0)
            {
                if(weaponRenderer.sortingOrder > weaponSortingLayerBehindBody)
                {
                    weaponRenderer.sortingOrder = weaponSortingLayerBehindBody;
                    CmdChangeWeaponSortingLayer(weaponSortingLayerBehindBody);
                }
            }
            else
            {
                if(weaponRenderer.sortingOrder < weaponSortingLayerAboveBody)
                {
                    weaponRenderer.sortingOrder = weaponSortingLayerAboveBody;
                    CmdChangeWeaponSortingLayer(weaponSortingLayerAboveBody);
                }
            }
        }

        [Command]
        private void CmdChangeWeaponSortingLayer(int newSortingLayer)
        {
            RpcChangeWeaponSortingLayer(newSortingLayer);

            [ClientRpc(includeOwner = false)]
            void RpcChangeWeaponSortingLayer(int newSortingLayer)
            {
                weaponRenderer.sortingOrder = newSortingLayer;
            }
        }
    }
}