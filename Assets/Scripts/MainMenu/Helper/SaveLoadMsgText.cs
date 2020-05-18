using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadMsgText : MonoBehaviour
{
    Text _text = null;
    float _resetTimer = 5.0f;

    public void SetText(string text)
    {
        _text.text = text;
        _resetTimer = 5.0f;
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        if (_text.text != string.Empty)
        {
            _resetTimer -= Time.deltaTime;
            if (_resetTimer <= 0.0f)
            {
                _text.text = string.Empty;
                _resetTimer = 5.0f;
            }
        }
    }
}
