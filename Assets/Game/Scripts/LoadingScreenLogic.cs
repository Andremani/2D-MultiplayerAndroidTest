using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class LoadingScreenLogic : MonoBehaviour
    {
        [Scene] [SerializeField] private string lobbyScene;

        void Start()
        {
            StartCoroutine(FakeLoading());
        }

        private IEnumerator FakeLoading()
        {
            float randomLoadingTime = Random.Range(0.5f, 1.4f);
            yield return new WaitForSeconds(randomLoadingTime);

            SceneManager.LoadScene(lobbyScene);
        }
    }
}