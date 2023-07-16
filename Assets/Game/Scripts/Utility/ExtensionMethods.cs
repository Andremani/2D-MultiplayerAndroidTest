using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest.Utility
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns Vector2 with all Absolute values of its components
        /// </summary>
        /// <param name="vector2"></param>
        public static Vector2 Abs(this Vector2 vector2)
        {
            return new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
        }
    }
}