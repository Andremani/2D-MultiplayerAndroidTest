using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Andremani.TwoDMultiplayerAndroidTest.UI
{
    public class UIEndGamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject visuals;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI coinsCountText;

        private void Start()
        {
            visuals.SetActive(true);
            gameObject.SetActive(false);

            GameManager.OnGameOver += OnGameEnd;
        }

        private void OnGameEnd(Player winner)
        {
            titleText.text = $"Player <i>{winner.Nickname}</i> win!";
            coinsCountText.text = winner.CollectablesSystem.Wealth.ToString();

            if(gameObject == null)
            {
                Debug.Log("GO is Null");
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            GameManager.OnGameOver -= OnGameEnd;
        }
    }
}