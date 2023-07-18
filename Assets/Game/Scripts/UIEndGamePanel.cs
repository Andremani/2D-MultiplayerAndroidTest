using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class UIEndGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI coinsCountText;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnGameEnd(Player winner)
        {
            titleText.text = $"Player {winner.Nickname} win!";
            coinsCountText.text = winner.CollectablesSystem.Wealth.ToString();

            gameObject.SetActive(true);
        }
    }
}