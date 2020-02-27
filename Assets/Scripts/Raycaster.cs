using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research.Unity;

public class Raycaster : MonoBehaviour
{
    IGazeData data;
    IEnumerator RaycastRoutine;

    private void Awake()
    {
        RaycastRoutine = MainRaycastRoutine();
    }

    private void Start()
    {
        OnStart();
    }

    private void OnStart()
    {
        StartCoroutine(RaycastRoutine);
    }

    IEnumerator MainRaycastRoutine()
    {

        while (true)
        {
            data = EyeTracker.Instance.LatestGazeData;
            RaycastHit2D hit2D; 
            if (data.CombinedGazeRayScreenValid)
            {
                Debug.Log("Eyedata valid");
                hit2D = Physics2D.Raycast(data.CombinedGazeRayScreen.origin, data.CombinedGazeRayScreen.direction);
                Debug.Log("Combined Origin:" + data.CombinedGazeRayScreen.origin + " | Combined dest: " + data.CombinedGazeRayScreen.direction);

                CheckHit2D(hit2D);
            }
            else if (data.Left.GazePointValid)
            {
                Debug.LogWarning("Right eye invalid");
                hit2D = Physics2D.Raycast(data.Left.GazeRayScreen.origin, data.Left.GazeRayScreen.direction);
                Debug.Log("Left Origin:" + data.Left.GazeRayScreen.origin + " | left dest: " + data.Left.GazeRayScreen.direction);
                CheckHit2D(hit2D);
            }
            else if (data.Right.GazePointValid)
            {
                Debug.LogWarning("Left eye invalid");
                hit2D = Physics2D.Raycast(data.Right.GazeRayScreen.origin, data.Right.GazeRayScreen.direction);
                Debug.Log("right Origin:" + data.Right.GazeRayScreen.origin + " | right dest: " + data.Right.GazeRayScreen.direction);
                CheckHit2D(hit2D);
            }
            else
            {
                Debug.Log("No Eye detected.");
            }
            yield return new WaitForEndOfFrame();
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
