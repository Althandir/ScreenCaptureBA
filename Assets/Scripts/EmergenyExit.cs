using UnityEngine;

public class EmergenyExit : MonoBehaviour
{
    [SerializeField] KeyCode ExitKey;
    bool _exitPressed;

    private void Awake()
    {
        if (ExitKey == KeyCode.None)
        {
            ExitKey = KeyCode.Escape;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(ExitKey))
        {
            _exitPressed = true;
        }

        if (_exitPressed)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
