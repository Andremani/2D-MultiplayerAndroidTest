using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Andremani.TwoDMultiplayerAndroidTest.UI
{
    public class PlayerCardUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI nicknameText;
        [SerializeField] private TextMeshProUGUI isReadyText;
        [SerializeField] private GameObject hostSign;
        [SerializeField] private GameObject kickSign;
        [SerializeField] private Button kickButton;
        [Header("Options")]
        [SerializeField] private string readyString;
        [SerializeField] private string notReadyString;

        private NetRoomPlayer connectedPlayer;

        public event Action<NetRoomPlayer> OnKickButtonClickEvent;

        private void OnEnable()
        {
            kickButton.onClick.AddListener(OnKickButtonClick);
        }

        private void OnDisable()
        {
            kickButton.onClick.RemoveListener(OnKickButtonClick);
        }

        public void SetPlayer(NetRoomPlayer netRoomPlayer)
        {
            connectedPlayer = netRoomPlayer;
            nicknameText.text = netRoomPlayer.Nickname;

            hostSign.SetActive(IsHostPlayer(netRoomPlayer));
            kickSign.SetActive(netRoomPlayer.isServer && IsHostPlayer(netRoomPlayer) || netRoomPlayer.isServerOnly);

            bool IsHostPlayer(NetRoomPlayer player)
            {
                if(player.index == 0) //may work incorrectly when there are no host at all (server-only)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SetReady(bool isReady)
        {
            if(isReady)
            {
                isReadyText.text = readyString;
            }
            else
            {
                isReadyText.text = notReadyString;
            }
        }

        private void OnKickButtonClick()
        {
            OnKickButtonClickEvent?.Invoke(connectedPlayer);
        }
    }
}