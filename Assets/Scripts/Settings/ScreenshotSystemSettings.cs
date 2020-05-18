using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research.Unity;

public class ScreenshotSystemSettings : MonoBehaviour
{
    private static ScreenshotSystemSettings _instance;

    private SettingsData _settingsData = new SettingsData();
    private string _saveDir = string.Empty;

    public int GridAccuracy
    {
        get => _settingsData.gridAccuracy;
        set
        {
            _settingsData.gridAccuracy = value;
        }
    }
    public float TimeDelay
    {
        get => _settingsData.timeDelay;
        set => _settingsData.timeDelay = value;
    }

    public string OwnerName
    {
        get => _settingsData.ownerName;
        set => _settingsData.ownerName = value;
    }

    public bool CatchHeatmap
    {
        get => _settingsData.catchHeatmap;
        set => _settingsData.catchHeatmap = value;
    }

    public bool CatchPupils
    {
        get => _settingsData.catchPupils;
        set => _settingsData.catchPupils = value;
    }

    public static ScreenshotSystemSettings Instance { get => _instance; }

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("Double ScreenshotSettings detected! Destroying duplicate!");
            Destroy(this.gameObject);
        }
        else
        {
            _saveDir = Application.persistentDataPath + "/settings";
            _instance = this;

            Application.runInBackground = true;
        }
    }

    private void Start()
    {
        if (!System.IO.Directory.Exists(_saveDir))
        {
            System.IO.Directory.CreateDirectory(_saveDir);
        }

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(EyeTracker.Instance);
        DontDestroyOnLoad(Calibration.Instance);
        DontDestroyOnLoad(TrackBoxGuide.Instance);
    }

    /// <summary>
    /// Method to save the settings. Returns true when successful, false if file already exists
    /// </summary>
    public bool SaveToDisk(string fileName)
    {
        if (!fileName.Contains(".json"))
        {
            fileName += ".json";
        }

        string totalPath = _saveDir + "/" + fileName;

        if (System.IO.File.Exists(totalPath))
        {
            return false;
        }
        else
        {
            Save(totalPath);
            return true;
        }
    }

    private void Save(string path)
    {
        string json = JsonUtility.ToJson(_settingsData);
        System.IO.File.WriteAllText(path, json);
        Debug.Log("Settings saved in: " + path);
    }

    /// <summary>
    /// Loads the given File into settings. Returns true if successful, false if File not found. 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public bool LoadFromDisk(string fileName)
    {
        if (!fileName.Contains(".json"))
        {
            fileName += ".json";
        }
        string totalPath = _saveDir + "/" + fileName;

        if (System.IO.File.Exists(totalPath))
        {
            Load(totalPath);
            return true;
        }
        else
        {
            Debug.LogError("Could not find specified file in: " + totalPath);
            return false;
        }
    }

    private void Load(string path)
    {
        SettingsData loadedData = JsonUtility.FromJson<SettingsData>(System.IO.File.ReadAllText(path));
        Instance._settingsData = loadedData;
        Debug.Log("Settings loaded from: " + path);
        Debug.Log(_settingsData);
    }
}

[System.Serializable]
public class SettingsData
{
    /// <summary>
    /// Name of the owner of the data
    /// </summary>
    public string ownerName;
    /// <summary>
    /// Accuracy of the EyetrackingGrid
    /// </summary>
    [Range(4, 10)]
    public int gridAccuracy;
    /// <summary>
    /// TimeDelay between each Screenshot
    /// </summary>
    [Range(0.5f, 2f)]
    public float timeDelay;
    /// <summary>
    /// Should heatmaps be created?
    /// </summary>
    public bool catchHeatmap;
    /// <summary>
    /// Should the pupil diameter be catched?
    /// </summary>
    public bool catchPupils;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
