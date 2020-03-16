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
    [SerializeField] bool _StartOnAwake;
    [Tooltip("Time between each Screenshot")]
    [Range(0.5f, 3)]
    [SerializeField] float _timeDelay = 0;

    [Header("Debug:")]
    [SerializeField] private bool _isActive;
    Process _screenCaptureProcess = new Process();
    IEnumerator _createScreenshotRoutine;

    public static ScreenshotSystemWin Instance;

    public float TimeDelay { get => _timeDelay;}

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
        // Referencing Routine
        _createScreenshotRoutine = CreateScreenshotCoroutine();
    }

    void Start()
    {
        if (ScreenshotSystemSettings.Instance)
        {
            // Read settings
            _timeDelay = ScreenshotSystemSettings.Instance.TimeDelay;
        }

        Application.runInBackground = true;

        // Start Coroutine
        if (_StartOnAwake)
        {
            StartCoroutine(StartScreenshotRoutine());
        }
    }

    private void OnApplicationQuit()
    {
        _isActive = false;
        StopAllCoroutines();
        Application.OpenURL(Application.persistentDataPath);
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
            if (_timeDelay < 0.5f)
            {
                UnityEngine.Debug.LogError("Could not start Screenshotroutine, because timeDelay is near zero. Would take tooo much ressources.");
                yield return null;
            }

            if (CheckForEyeTracker())
            {
                StartCoroutine(_createScreenshotRoutine);
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
        if (_isActive)
        {
            // Prevents duplicated activation of Routine
            yield return null;
        }
        else
        {
            _isActive = true;
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
                    yield return new WaitForSeconds(_timeDelay);
                    UnityEngine.Debug.Log("Waited " + _timeDelay + " seconds.");
                    UnityEngine.Debug.Log("Capturing Screen...");
                    _screenCaptureProcess.StartInfo.FileName = Application.streamingAssetsPath + "/ScreenCapture.exe";
                    _screenCaptureProcess.Start();

                    yield return new WaitForSeconds(0.5f);

                    ScreenshotImporter.Instance.ImportScreenshot();
                    UnityEngine.Debug.Log("Screen imported...");
                    ScreenshotCamCapture.Instance.CaptureWithHeatmap();
                    UnityEngine.Debug.Log("Screen with EyetrackingUI saved.");
                }
            }
        }
    }


}
