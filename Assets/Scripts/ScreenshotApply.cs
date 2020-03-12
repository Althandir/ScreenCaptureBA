using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenshotApply : MonoBehaviour
{
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();

    }

    private void LateUpdate()
    {
        _image.sprite = ScreenshotImporter.Instance.Output;
        _image.type = Image.Type.Simple;
    }
}
