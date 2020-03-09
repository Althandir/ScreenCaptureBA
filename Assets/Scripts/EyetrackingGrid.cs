using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws the EyetrackingGrid on the imported screenshot.
/// </summary>
public class EyetrackingGrid : MonoBehaviour
{
    [SerializeField] GameObject _EyetrackableTile = null;
    [Range(4, 10)]
    [SerializeField] int _accuracy = 0;
    int _counter = 0;

    void Start()
    {
        ReadSettings();
        SetupGrid();
    }

    void ReadSettings()
    {
        if (_accuracy == 0 && ScreenshotSystemSettings.Instance)
        {
            _accuracy = ScreenshotSystemSettings.Instance.GridAccuracy;
        }
        else
        {
            _accuracy = 10;
            Debug.LogError("Could not find ScreenshotSystemSettings. Using max Value.");
        }
    }

    void SetupGrid()
    {
        float[] xMinValues = new float[_accuracy];
        float[] yMinValues = new float[_accuracy];

        float[] xMaxValues = new float[_accuracy];
        float[] yMaxValues = new float[_accuracy];

        for (int i = 0; i < _accuracy; i++)
        {
            xMinValues[i] = (float)i / _accuracy;
            yMinValues[i] = (float)i / _accuracy;

            xMaxValues[i] = (float)(i + 1) / _accuracy;
            yMaxValues[i] = (float)(i + 1) / _accuracy;
        }
        for (int i = 0; i < _accuracy; ++i)
        {
            for (int k = 0; k < _accuracy; ++k)
            {
                GameObject tile = Instantiate(_EyetrackableTile, this.transform);

                tile.name = "Tile_" + _counter;
                _counter += 1;

                RectTransform rectTransform = tile.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(xMinValues[i], yMinValues[k]);
                rectTransform.anchorMax = new Vector2(xMaxValues[i], yMaxValues[k]);
            }

        }
    }
}
