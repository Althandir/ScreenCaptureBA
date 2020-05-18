using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Class to be derrived from for opening the Explorer.
/// Base Idea from: https://answers.unity.com/questions/43422/how-to-implement-show-in-explorer.html
/// </summary>
public class OpenExplorer : MonoBehaviour
{
    protected string _path;

    [ContextMenu("Debug_Open")]
    public void Open()
    {
        if (_path == string.Empty)
        {
            Debug.Log("No Path specified. Opening persistent data folder ...");
            Application.OpenURL(Application.persistentDataPath);
        }
        else
        {
            Application.OpenURL(_path);
        }
    }
}
