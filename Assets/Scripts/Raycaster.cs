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
            if (data.CombinedGazeRayScreenValid)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(data.CombinedGazeRayScreen.origin, data.CombinedGazeRayScreen.direction);
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
            else
            {
                Debug.LogError("Invalid GazeData");
            }
           
            yield return new WaitForEndOfFrame();
        }
        

    }

}
