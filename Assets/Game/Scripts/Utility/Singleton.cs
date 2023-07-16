using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
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

        public virtual void Awake()
        {
            if (!Exists)
            {
                instance = this as T;
            }
        }

        public void OnDestroy()
        {
            instance = null;
        }
    }
}