using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest.Utility
{
    public abstract class NetworkSingleton<T> : NetworkBehaviour where T : NetworkSingleton<T>
    {
        private static T instance;

        public static T I
        {
            get
            {
                //#if UNITY_EDITOR
                if (!Application.isPlaying && !Exists)
                {
                    instance = FindObjectOfType<T>();
                }
                //#endif
                return instance;
            }
        }

        public static bool Exists => instance != null;

        protected virtual void Awake()
        {
            if (!Exists)
            {
                instance = this as T;
            }
        }

        protected void OnDestroy()
        {
            instance = null;
        }
    }
}