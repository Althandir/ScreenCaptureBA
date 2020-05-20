using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tobii.Research.Unity;

[RequireComponent(typeof(Button))]
public class SceneChanger : MonoBehaviour
{
    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
        button.interactable = false;
    }

    private void Start()
    {
        StartCoroutine(Checker());
    }

    private void OnButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator Checker()
    {

        bool TrackBoxChecked = false;
        while (!Calibration.Instance.LatestCalibrationSuccessful && !TrackBoxChecked)
        {
            yield return new WaitForSeconds(0.25f);
            if (TrackBoxGuide.Instance.TrackBoxGuideActive)
            {
                TrackBoxChecked = true;
            }
        }
        button.interactable = true;
    }
}
