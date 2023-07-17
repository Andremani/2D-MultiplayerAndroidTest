using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class Coin : MonoBehaviour
    {
        [field: SerializeField] public int Value { get; private set; }

        public void Disappear()
        {
            Destroy(gameObject);
        }
    }
}