using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class EyetrackableTile : MonoBehaviour
{
    Image image;


    
    #region UnityFunctions

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    #endregion

    public void ChangeColor()
    {
        image.color = image.color + new Color(0.01f, 0, 0, 0);
    }

}
