﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research.Unity;

public class Raycaster : MonoBehaviour
{
    IGazeData _data;
    IEnumerator _raycastRoutine;

    private void Awake()
    {
        _raycastRoutine = MainRaycastRoutine();
    }

    private void Start()
    {
        OnStart();
    }

    private void OnStart()
    {
        StartCoroutine(_raycastRoutine);
    }

    IEnumerator MainRaycastRoutine()
    {
        while (true)
        {
            if (Calibration.Instance.CalibrationInProgress || TrackBoxGuide.Instance.TrackBoxGuideActive)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                _data = EyeTracker.Instance.LatestGazeData;
                RaycastHit2D hit2D;
                if (_data.CombinedGazeRayScreenValid)
                {
                    Debug.Log("Eyedata valid");
                    hit2D = Physics2D.Raycast(_data.CombinedGazeRayScreen.origin, Vector2.zero );
                    Debug.DrawLine(_data.CombinedGazeRayScreen.origin, Vector2.zero, Color.red, 100);
                    CheckHit2D(hit2D);
                }
                else if (_data.Left.GazePointValid)
                {
                    Debug.LogWarning("Right eye invalid");
                    hit2D = Physics2D.Raycast(_data.Left.GazeRayScreen.origin, Vector2.zero);

                    CheckHit2D(hit2D);
                }
                else if (_data.Right.GazePointValid)
                {
                    Debug.LogWarning("Left eye invalid");
                    hit2D = Physics2D.Raycast(_data.Right.GazeRayScreen.origin, Vector2.zero);
                    CheckHit2D(hit2D);
                }
                else
                {
                    Debug.Log("No Eye detected.");
                }
                yield return new WaitForEndOfFrame();
            }
        }
        

    }

    void CheckHit2D(RaycastHit2D hit2D)
    {
        // If Raycast hit a 2DCollider...
        if (hit2D)
        {
            GameObject hitObject = hit2D.collider.gameObject;
            // ... print GameObject name
            Debug.Log(hitObject.name);
            // if GameObject is a EyetrackableTile ...
            if (hitObject.GetComponent<EyetrackableTile>())
            {
                // TODO: Colorgrade Tile 
                hitObject.GetComponent<EyetrackableTile>().ChangeColor();
            }
        }
    }
}
