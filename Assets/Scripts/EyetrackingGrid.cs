using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws the EyetrackingGrid on the imported screenshot.
/// </summary>
public class EyetrackingGrid : MonoBehaviour
{
    [SerializeField] GameObject EyetrackableTile = null;
    [Range(4, 10)]
    [SerializeField] int accuracy = 10;
    int counter = 0;

    void Start()
    {
        float[] xMinValues = new float[accuracy];
        float[] yMinValues = new float[accuracy];

        float[] xMaxValues = new float[accuracy];
        float[] yMaxValues = new float[accuracy];

        for (int i = 0; i < accuracy; i++)
        {
            xMinValues[i] = (float) i / accuracy;
            yMinValues[i] = (float) i / accuracy;

            xMaxValues[i] = (float)(i + 1) / accuracy;
            yMaxValues[i] = (float)(i + 1) / accuracy;
        }
        for (int i = 0; i < accuracy; ++i)
        {
            for (int k = 0; k < accuracy; ++k)
            {
                GameObject tile = Instantiate(EyetrackableTile, this.transform);

                tile.name = "Tile_" + counter;
                counter += 1;

                RectTransform rectTransform = tile.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(xMinValues[i], yMinValues[k]);
                rectTransform.anchorMax = new Vector2(xMaxValues[i], yMaxValues[k]);
            }

        }

    }
}
