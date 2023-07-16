using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andremani.TwoDMultiplayerAndroidTest.Utility;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class UIManager : Singleton<UIManager>
    {
        [field: SerializeField] public Joystick LeftJoystick { get; private set; }
        [field: SerializeField] public Joystick RightJoystick { get; private set; }
    }
}