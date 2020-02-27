using System.Collections;
using System;
using System.IO;
using UnityEngine;

public class ScreenshotCamCapture : MonoBehaviour
{
    static int count;

    string totalPath;
    string screenshotPath;
    Texture2D texture;
    byte[] screenshotBytes;
    public static ScreenshotCamCapture Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Double ScreenshotCamCapture detected!");
            Destroy(this);
        }

        screenshotPath = Application.dataPath + "/Screenshots";

        if (!Directory.Exists(screenshotPath))
        {
            Directory.CreateDirectory(screenshotPath);
        }
    }

    public void CaptureWithEyetrackingUI()
    {
        StartCoroutine(Capture());
    }

    private IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        
        texture = ScreenCapture.CaptureScreenshotAsTexture();

        screenshotBytes = texture.EncodeToPNG();

        totalPath = screenshotPath + "/_" + count + ".png";

        File.WriteAllBytes(totalPath, screenshotBytes);
        
        Array.Clear(screenshotBytes, 0, screenshotBytes.Length);
        
        Resources.UnloadUnusedAssets();

        count += 1;
    }
}
