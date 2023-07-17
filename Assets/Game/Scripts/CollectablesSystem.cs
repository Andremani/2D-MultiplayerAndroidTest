using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class CollectablesSystem : MonoBehaviour
    {
        [SerializeField] private TriggerNotifier triggerNotifier;

        private int wealth;

        void Start()
        {
            triggerNotifier.OnTriggerEnterEvent += OnTriggerEntering;
        }

        private void OnTriggerEntering(Collider2D otherCollider)
        {
            if(otherCollider.TryGetComponent<Coin>(out Coin coin))
            {
                wealth += coin.Value;
                coin.Disappear();

                Debug.Log(wealth);
            }
        }
    }
}