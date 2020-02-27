using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenshotApply : MonoBehaviour
{
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        image.sprite = ScreenshotImporter.Instance.Output;
        image.type = Image.Type.Tiled;
    }
}
