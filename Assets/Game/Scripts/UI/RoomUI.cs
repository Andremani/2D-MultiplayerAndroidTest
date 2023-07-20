using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.UI
{
    public class RoomUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button quitButton;
        [SerializeField] private TextMeshProUGUI quitButtonText;
        [SerializeField] private Transform playerCardsParent;
        [Space]
        [SerializeField] private PlayerCardUI playerCardPrefab;
        [Header("Options")]
        [SerializeField] private string quitTextOnClient;
        [SerializeField] private string quitTextOnHostOrServer;

        private Dictionary<NetRoomPlayer, PlayerCardUI> playersUIs = new Dictionary<NetRoomPlayer, PlayerCardUI>();

        private void Start()
        {
            //if(!(NetworkRoomManager.singleton is NetRoomManager netRoomManager))
            //{
            //    return;
            //}

            //Debug.Log(netRoomManager.netRoomPlayers.Count);
            //foreach (NetRoomPlayer player in netRoomManager.netRoomPlayers)
            //{
                
            //}

            //netRoomManager.OnClientPlayerNumberChanged += OnClientPlayerNumberChanged;
        }

        //private void OnClientPlayerNumberChanged()
        //{
        //    if (!(NetworkRoomManager.singleton is NetRoomManager netRoomManager))
        //    {
        //        return;
        //    }

        //    Debug.Log("OnClientPlayerNumberChanged (enter) - count in roomslots - " + netRoomManager.roomSlots.Count);

        //    //Setup(netRoomManager.roomSlots[0]);
        //    foreach(NetRoomPlayer player in playersUIs.Keys)
        //    {
        //        Destroy(playersUIs[player].gameObject);
        //    }
        //    playersUIs.Clear();

        //    foreach (NetRoomPlayer player in netRoomManager.roomSlots)
        //    {
        //        PlayerCardUI playerCardUI = Instantiate(playerCardPrefab, playerCardsParent);
        //        playersUIs.Add(player, playerCardUI);

        //        playerCardUI.SetPlayer(player);
        //        playerCardUI.SetReady(false);
        //        playerCardUI.OnKickButtonClickEvent += player.CmdKickPlayer;
        //    }
        //}

        public void Setup(NetRoomPlayer netRoomPlayer)
        {
            if(netRoomPlayer.isClientOnly)
            {
                quitButtonText.text = quitTextOnClient;
            }
            else
            {
                quitButtonText.text = quitTextOnHostOrServer;
            }
        }


    }
}