using System;
using System.IO;
using UnityEngine;

/// <summary>
/// System to Import Screenshot from ScreenCapture
/// Sources & Ideas: 
/// https://answers.unity.com/questions/815942/screenshot-of-users-desktop-upon-launch.html
/// </summary>
public class ScreenshotImporter : MonoBehaviour
{
    private bool _lockFile;
    private Sprite _outputSprite;
    private bool _isDebug;
    private string _path;
    public Sprite Output { get => _outputSprite;}
    public bool Locked { get => _lockFile;}

    public static ScreenshotImporter Instance;


    private void Awake()
    {
        #if UNITY_EDITOR
        _isDebug = true;
        #else
        _isDebug = false;
        #endif

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Double ScreenshotImporter detected!");
            Destroy(this);
        }
        // TODO::Problem with Build: Paths are not correct. In Build: Screenshot is saved one Directory above Application.dataPath

        _path = Application.streamingAssetsPath + "/LatestScreenshot.png";

    }

    /// <summary>
    /// Reading screenshot from disk. 
    /// </summary>
    /// <remarks>
    /// High Risk of Memory Leaks. 
    /// See:
    /// https://docs.unity3d.com/Packages/com.unity.memoryprofiler@0.1/manual/workflow-memory-leaks.html
    /// Sources & Ideas
    /// https://stackoverflow.com/questions/46482323/capture-and-save-screenshot-with-screencapture-capturescreenshot
    /// </remarks>
    public void ImportScreenshot()
    {
        try
        {
            _lockFile = true;

            byte[] latestScreenshotBytes = File.ReadAllBytes(_path);

            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.LoadImage(latestScreenshotBytes);
            // Array.Clear to prevent MemoryLeak inside Unity
            Array.Clear(latestScreenshotBytes, 0, latestScreenshotBytes.Length);
            _lockFile = false;
            // /6 to prevent Exception at Sprite.Create if Captured Screen is to small
            _outputSprite = Sprite.Create(texture, new Rect(0, 0, Screen.width/6, Screen.height/6), Vector2.zero,100);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        
    }


}
