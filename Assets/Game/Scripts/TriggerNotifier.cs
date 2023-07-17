using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class TriggerNotifier : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggerEnterEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnterEvent?.Invoke(collision);
        }
    }
}
