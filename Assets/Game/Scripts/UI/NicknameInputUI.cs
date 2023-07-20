using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Andremani.TwoDMultiplayerAndroidTest.UI
{
    public class NicknameInputUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nicknameInputField;

        //public static event Action<string> OnNicknameChanged;

        public static string nickname;

        private void Start()
        {
            nicknameInputField.onValueChanged.AddListener(
                (value) =>
                {
                    nickname = value;
                //OnNicknameChanged?.Invoke(value);
            });
        }
    }
}