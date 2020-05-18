using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class ScreenshotCamCapture : MonoBehaviour
{
    static int _count;

    string _screenshotPath;
    string _path;
    Texture2D _texture;
    byte[] _screenshotBytes;
    float _timeStamp;

    UnityEvent _onSaveEvent = new UnityEvent();

    private static ScreenshotCamCapture _instance;

    public UnityEvent OnSaveEvent { get => _onSaveEvent; }
    public static ScreenshotCamCapture Instance { get => _instance; }
    public float TimeStamp { get => _timeStamp; }
    public string Path { get => _path; }

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
    }

    private void Start()
    {
        _timeStamp = ScreenshotSystemWin.Instance.TimeDelay;
        // Idea from: https://stackoverflow.com/questions/22225044/creating-a-new-folder-with-todays-date-on-specific-folder
        if (ScreenshotSystemSettings.Instance)
        {
            _path = Application.persistentDataPath +"/"+ ScreenshotSystemSettings.Instance.OwnerName;
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        else
        {
            _path = Application.persistentDataPath + "/debug";
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        string dateTime = DateTime.Now.ToString("dd-MM-yyyy_hh-mm");
        _path += "/" + dateTime;
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }

    public void CaptureWithHeatmap()
    {
        StartCoroutine(Capture());
    }

    private IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        
        _texture = ScreenCapture.CaptureScreenshotAsTexture();

        _screenshotBytes = _texture.EncodeToPNG();

        _screenshotPath = _path + "/_num" + _count + "_sec" + _timeStamp + ".png";

        File.WriteAllBytes(_screenshotPath, _screenshotBytes);

        _onSaveEvent.Invoke();

        Array.Clear(_screenshotBytes, 0, _screenshotBytes.Length);
        
        Resources.UnloadUnusedAssets();

        _count += 1;

        _timeStamp += ScreenshotSystemWin.Instance.TimeDelay;
    }
}
