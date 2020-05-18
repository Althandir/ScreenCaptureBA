using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadManager : UI_Serialization
{
    protected override void HandleButtonPressed()
    {
        if (_inputField.text == string.Empty)
        {
            Debug.Log("No Filename in Inputfield.");
        }
        else
        {
            if(!ScreenshotSystemSettings.Instance.LoadFromDisk(_inputField.text))
            {
                _msgText.SetText("File: <" + _inputField.text + "> not found.");
            }
            else
            {
                _msgText.SetText("<" + _inputField.text + "> loaded.");
            }
        }
    }
}
