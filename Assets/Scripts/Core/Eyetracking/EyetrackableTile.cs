using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(BoxCollider2D), typeof(RectTransform))]
public class EyetrackableTile : MonoBehaviour
{
    RectTransform _rt;
    BoxCollider2D _col2D;
    Image _image;

    bool _hasReachedMaxRed;
    bool _hasReachedMaxYellow;
    bool _hasReachedMaxGreen;

    [Range (0.01f, 0.015f)]
    [SerializeField] float _colorInterval;

    #region UnityFunctions

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _col2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SetupCollider();
        ScreenshotCamCapture.Instance.OnSaveEvent.AddListener(OnSaveHandler);

        _colorInterval = _colorInterval / ScreenshotSystemWin.Instance.TimeDelay;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            SetupCollider();
        }
    }
    #endregion

    private void OnSaveHandler()
    {
        _image.color = new Color32(0, 0, 0, 80);
        _hasReachedMaxGreen = false;
        _hasReachedMaxYellow = false;
        _hasReachedMaxRed = false;
    }

    private void SetupCollider()
    {
        _col2D.size = _rt.rect.size;
    }



    public void ChangeColor()
    {
        if (!_hasReachedMaxGreen)
        {
            if (_image.color.g < 0.5f)
            {
                _image.color += new Color(0, 0.7f, 0, 0);
            }
            else
            {
                _image.color += new Color(0, _colorInterval, 0, 0);
            }

            if (_image.color.g >= 0.8f)
            {
                _hasReachedMaxGreen = true;
            }
        }
        else if (!_hasReachedMaxYellow)
        {
            _image.color += new Color(_colorInterval, 0, 0, 0);
            if (_image.color.r >= 0.8f)
            {
                _hasReachedMaxYellow = true;
            }
        }
        else if (!_hasReachedMaxRed)
        {
            _image.color -= new Color(0, _colorInterval, 0, 0);
            if (_image.color.g <= 0.0f)
            {
                _hasReachedMaxRed = true;
            }
        }
    }

}
