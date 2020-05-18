using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenResultsExplorer : OpenExplorer
{
    private void Awake()
    {
        _path = Application.persistentDataPath;
    }
}
