using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardButton : MonoBehaviour
{
    public Keyboard keyboard;
    TMP_Text _buttonText;
    
    void Start()
    {
        keyboard = GetComponentInParent<Keyboard>();
        _buttonText = GetComponentInChildren<TMP_Text>();
        if ( _buttonText.text.Length == 1 ) {
            GameToButtonText();
        }
    }

    public void GameToButtonText() {
        if (keyboard.caps)
            _buttonText.text = gameObject.name.ToUpper();
        else
            _buttonText.text = gameObject.name.ToLower();
        
        GetComponentInChildren<ButtonVR>()
            .onRelease.RemoveAllListeners();
        GetComponentInChildren<ButtonVR>()
            .onRelease.AddListener(delegate { keyboard.InsertChar(_buttonText.text); });
    }
}
