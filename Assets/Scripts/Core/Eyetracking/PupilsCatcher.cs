using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research.Unity;
using System.IO;

/// <summary>
/// Class to save pupil data into a csv file. 
/// Idea & Source from: https://shanemartin2797blog.wordpress.com/2015/11/20/how-to-read-from-and-write-to-csv-in-unity/
/// </summary>
public class PupilsCatcher : MonoBehaviour
{
    string _path;
    const string _header = "Timestamp. LeftEye. RightEye";
    const string _fileName = "PupilSize.csv";

    StreamWriter _writer;

    private void Awake()
    {
        if (!ScreenshotSystemSettings.Instance)
        {
            Debug.LogError("No settings found.");
        }
    }

    private void Start()
    {
        ScreenshotCamCapture.Instance.OnSaveEvent.AddListener(OnSaveHandler);

        //TODO::Summorize Path into Settings or System

        if (!ScreenshotSystemSettings.Instance)
        {
            // _path = Application.persistentDataPath + "/debug/" + _fileName;
            StartCoroutine(GetPath());
        }
        else if (ScreenshotSystemSettings.Instance.CatchPupils)
        {
            // _path = Application.persistentDataPath + "/" + ScreenshotSystemSettings.Instance.OwnerName +"/"+ _fileName;
        }
        else if (!ScreenshotSystemSettings.Instance.CatchPupils)
        {
            Debug.Log("Settings found. Disabling Pupilscatcher...");
            Destroy(this);
        }
        /*
        _writer = new StreamWriter(_path);
        _writer.WriteLine("sep=.");
        _writer.WriteLine(_header);
        */
    }

    IEnumerator GetPath()
    {
        yield return new WaitForSeconds(1);
        _path = ScreenshotCamCapture.Instance.Path;
        _writer = new StreamWriter(_path +"/"+_fileName);
        _writer.WriteLine("sep=.");
        _writer.WriteLine(_header);
    }

    private void OnSaveHandler()
    {
        string leftDiameter;
        string rightDiameter;
        IGazeData gazeData = EyeTracker.Instance.LatestGazeData;
        if (gazeData.Left.PupilDiameterValid)
        {
            leftDiameter = gazeData.Left.PupilDiameter.ToString();
        }
        else
        {
            leftDiameter = "invalid";
        }
        if (gazeData.Right.PupilDiameterValid)
        {
            rightDiameter = gazeData.Right.PupilDiameter.ToString();
        }
        else
        {
            rightDiameter = "invalid";
        }

        if (_writer != null)
        {
            _writer.WriteLine(ScreenshotCamCapture.Instance.TimeStamp + "." + leftDiameter + "." + rightDiameter);
        }
        
    }

    private void OnApplicationQuit()
    {
        _writer.Close();
    }
}
