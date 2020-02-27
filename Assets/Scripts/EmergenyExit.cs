using UnityEngine;

public class EmergenyExit : MonoBehaviour
{
    [SerializeField] KeyCode ExitKey;
    bool exitPressed;

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
            exitPressed = true;
        }

        if (exitPressed)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
