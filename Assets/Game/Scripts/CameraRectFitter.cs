using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class CameraRectFitter : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        [SerializeField] private Rect rect;

        void Start()
        {
            float aspectRatio = (float)Screen.width / Screen.height;
            float targetAspectRatio = rect.width / rect.height;
            float newOrthographicSize;

            //ortho for fitting by height -> rect.height / 0.5f
            //ortho for fitting by width -> rect.width / aspectRatio * 0.5f

            if (aspectRatio >= targetAspectRatio)
            {
                newOrthographicSize = rect.height * 0.5f;
            }
            else
            {
                newOrthographicSize = rect.width / aspectRatio * 0.5f;
            }

            targetCamera.orthographicSize = newOrthographicSize;
        }
    }
}