using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.UI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private NetRoomManager netRoomManager;
        [Space]
        [SerializeField] private GameObject nicknameEnterView;
        [SerializeField] private GameObject gameStartView;
        [Space]
        [SerializeField] private NicknameInputUI nicknameInput;
        [SerializeField] private Button confirmNicknameButton;
        [Space]
        [SerializeField] private Button hostGameButton;
        [SerializeField] private Button joinGameButton;
        [SerializeField] private TMP_InputField ipInputField;
        [Space]
        [SerializeField] private GameObject connectingView;

        private bool tryingToJoinGame = false;
        private bool TryngToJoinGame
        {
            get { return tryingToJoinGame; }
            set
            {
                tryingToJoinGame = value;
                connectingView.SetActive(value);
            }
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(NicknameInputUI.nickname))
            {
                nicknameEnterView.SetActive(true);
                gameStartView.SetActive(false);
            }
            else
            {
                GoToGameStartView();
            }
        }

        private void OnEnable()
        {
            netRoomManager.OnClientConnectEvent += OnClientConnect;
            netRoomManager.OnClientDisconnectEvent += OnClientDisconnect;

            confirmNicknameButton.onClick.AddListener(GoToGameStartView);

            hostGameButton.onClick.AddListener(HostGame);
            joinGameButton.onClick.AddListener(TryJoinGame);
        }

        private void OnDisable()
        {
            netRoomManager.OnClientConnectEvent -= OnClientConnect;
            netRoomManager.OnClientDisconnectEvent -= OnClientDisconnect;

            confirmNicknameButton.onClick.RemoveListener(GoToGameStartView);

            hostGameButton.onClick.RemoveListener(HostGame);
            joinGameButton.onClick.RemoveListener(TryJoinGame);
        }

        private void GoToGameStartView()
        {
            nicknameEnterView.SetActive(false);
            gameStartView.SetActive(true);
        }

        private void HostGame()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                NetworkManager.singleton.StartHost();
            }
        }

        private void TryJoinGame()
        {
            TryngToJoinGame = true;

            NetworkManager.singleton.networkAddress = ipInputField.text;
            ipInputField.interactable = false;

            //TODO - StartClient in nextFrame for immidiate "Connecting..." appearance
            NetworkManager.singleton.StartClient();
        }

        private void OnClientConnect()
        {
            gameStartView.SetActive(false);

            TryngToJoinGame = false;
        }

        private void OnClientDisconnect()
        {
            ipInputField.interactable = true;
            gameStartView.SetActive(true);

            TryngToJoinGame = false;
        }
    }
}