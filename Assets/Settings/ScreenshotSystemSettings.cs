using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenshotSystemSettings : MonoBehaviour
{
    private static ScreenshotSystemSettings m_instance;

    private SettingsData m_settingsData = new SettingsData();
    private string m_saveDir = string.Empty;

    public int GridAccuracy
    {
        get => m_settingsData.gridAccuracy;
        set
        {
            m_settingsData.gridAccuracy = value;
        }
    }
    public static ScreenshotSystemSettings Instance { get => m_instance; set => m_instance = value; }

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("Double ScreenshotSettings detected! Destroying duplicate!");
            Destroy(this.gameObject);
        }
        else
        {
            m_saveDir = Application.persistentDataPath + "/settings";
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        if (!System.IO.Directory.Exists(m_saveDir))
        {
            System.IO.Directory.CreateDirectory(m_saveDir);
        }
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

        string totalPath = m_saveDir + "/" + fileName;

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
        string json = JsonUtility.ToJson(m_settingsData);
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
        string totalPath = m_saveDir + "/" + fileName;

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
        Instance.m_settingsData = loadedData;
        Debug.Log("Settings loaded from: " + path);
        Debug.Log(m_settingsData);
    }
}

[System.Serializable]
public class SettingsData
{
    /// <summary>
    /// Accuracy of the EyetrackingGrid
    /// </summary>
    [Range(4, 10)]
    public int gridAccuracy = 10;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
