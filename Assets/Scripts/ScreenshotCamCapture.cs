using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class ScreenshotCamCapture : MonoBehaviour
{
    static int _count;

    string _totalPath;
    string _screenshotPath;
    Texture2D _texture;
    byte[] _screenshotBytes;

    UnityEvent _onSaveEvent = new UnityEvent();

    private static ScreenshotCamCapture _instance;

    public UnityEvent OnSaveEvent { get => _onSaveEvent; }
    public static ScreenshotCamCapture Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("Double ScreenshotCamCapture detected!");
            Destroy(this);
        }

        _screenshotPath = Application.dataPath + "/Screenshots";

        if (!Directory.Exists(_screenshotPath))
        {
            Directory.CreateDirectory(_screenshotPath);
        }
    }

    public void CaptureWithEyetrackingUI()
    {
        StartCoroutine(Capture());
    }

    private IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        
        _texture = ScreenCapture.CaptureScreenshotAsTexture();

        _screenshotBytes = _texture.EncodeToJPG();

        _totalPath = _screenshotPath + "/_" + _count + ".jpg";

        File.WriteAllBytes(_totalPath, _screenshotBytes);

        _onSaveEvent.Invoke();

        Array.Clear(_screenshotBytes, 0, _screenshotBytes.Length);
        
        Resources.UnloadUnusedAssets();

        _count += 1;
    }
}
