using System.Collections;
using System;
using System.Diagnostics;
using UnityEngine;
using Tobii.Research.Unity;

//[RequireComponent(typeof(ScreenshotImporter))]
public class ScreenshotSystemWin : MonoBehaviour
{
    [Header("Settings:")]
    [Tooltip("If the ScreenshotSystem should be started on immediately")]
    public bool StartOnAwake;
    [Tooltip("Time between each Screenshot")]
    [Range(1, 3)]
    [SerializeField] float timeDelay = 1;

    [Header("Debug:")]
    [SerializeField] private bool isActive;
    Process ScreenCaptureProcess = new Process();
    IEnumerator CreateScreenshotRoutine;

    public static ScreenshotSystemWin Instance;
    #region UnityFunctions
    private void Awake()
    {
        // Setting up Instance
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Double ScreenshotSystemWin detected!");
            Destroy(this);
        }
        // Referencing Routine for stopping
        CreateScreenshotRoutine = CreateScreenshotCoroutine();
    }

    void Start()
    {
        Application.runInBackground = true;
        

        if (StartOnAwake)
        {
            StartCoroutine(StartScreenshotRoutine());
        }
    }

    private void OnApplicationQuit()
    {
        isActive = false;
        StopAllCoroutines();
    }
    #endregion
    
    /// <summary>
    /// Checks if the Eyetracker is active and ready to be used.
    /// </summary>
    bool CheckForEyeTracker()
    {
        if (!EyeTracker.Instance.Connected)
        {
            UnityEngine.Debug.Log("Eyetracker not found.");
            return false;
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// Method to start the MainRoutine. Called by Start()
    /// </summary>
    public IEnumerator StartScreenshotRoutine()
    {
        while (true)
        {
            if (timeDelay < 1)
            {
                UnityEngine.Debug.LogError("Could not start Screenshotroutine, because timeDelay is near zero. Would take tooo much ressources.");
                yield return null;
            }

            if (CheckForEyeTracker())
            {
                StartCoroutine(CreateScreenshotRoutine);
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    /// <summary>
    /// MainRoutine of the ScreenshotSystem
    /// </summary>
    IEnumerator CreateScreenshotCoroutine()
    {
        if (isActive)
        {
            // Prevents duplicated activation of Routine
            yield return null;
        }
        else
        {
            isActive = true;
            while (true)
            {
                // If ScreenshotImporter is not ready or Systen is in Calibrationmode or System displays the Trackbox
                // TODO: Create UI System for this! 
                if (ScreenshotImporter.Instance.Locked || Calibration.Instance.CalibrationInProgress || TrackBoxGuide.Instance.TrackBoxGuideActive)
                {
                    UnityEngine.Debug.LogWarning("ScreenImporter not ready || Calibration active || Trackbox active");
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(timeDelay);
                    UnityEngine.Debug.Log("Waited " + timeDelay + " seconds.");
                    UnityEngine.Debug.Log("Capturing Screen...");
                    ScreenCaptureProcess.StartInfo.FileName = Application.streamingAssetsPath + "/ScreenCapture.exe";
                    ScreenCaptureProcess.Start();

                    yield return new WaitForSeconds(0.5f);

                    ScreenshotImporter.Instance.ImportScreenshot();
                    UnityEngine.Debug.Log("Screen imported...");
                    ScreenshotCamCapture.Instance.CaptureWithEyetrackingUI();
                    UnityEngine.Debug.Log("Screen with EyetrackingUI saved.");
                }
            }
        }
    }


}
