using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UISaveManager : UI_Serialization
{
    protected override void HandleButtonPressed()
    {
        if (_inputField.text == string.Empty)
        {
            Debug.Log("No Filename in Inputfield.");
        }
        else
        {
            if (!ScreenshotSystemSettings.Instance.SaveToDisk(_inputField.text))
            {
                _msgText.SetText("<" + _inputField.text + "> already exists.");
            }
            else
            {
                _msgText.SetText("Saved as <" + _inputField.text + ">");
            }
        }

    }
}
