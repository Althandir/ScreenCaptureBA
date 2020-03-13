using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingsExplorer : OpenExplorer
{
    private void Awake()
    {
        _path = Application.persistentDataPath + "/settings";
    }
}
