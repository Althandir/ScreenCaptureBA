using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class to be derived from for to UI of Save&Loading the settings
/// </summary>
public class UI_Serialization : MonoBehaviour
{
    [SerializeField] protected Button _button = null;
    [SerializeField] protected InputField _inputField = null;
    [SerializeField] protected SaveLoadMsgText _msgText = null;

    private void Start()
    {
        _button.onClick.AddListener(HandleButtonPressed);
    }

    protected virtual void HandleButtonPressed()
    {
        Debug.LogError("Base Class of: " + this.name + "called. Use UISaveManager or UILoadManager instead.");
    }
}
