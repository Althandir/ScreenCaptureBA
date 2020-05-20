using MainMenuEnum;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Research.Unity;

[RequireComponent(typeof(Text))]
public class GetValueFromSetting : MonoBehaviour
{
    Text text;
    [SerializeField] TargetValue targetValue;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        switch (targetValue)
        {
            case TargetValue.heatmap:
                SetText(ScreenshotSystemSettings.Instance.CatchHeatmap);
                break;
            case TargetValue.pupildata:
                SetText(ScreenshotSystemSettings.Instance.CatchPupils);
                break;
            case TargetValue.CalibrationComplete:
                SetText(Calibration.Instance.LatestCalibrationSuccessful);
                break;
            case TargetValue.TrackboxComplete:
                if (TrackBoxGuide.Instance.TrackBoxGuideActive)
                {
                    SetText(true);
                }
                break;
            default:
                Debug.LogError("Missing targetValue in:" + transform.parent.parent.name);
                this.enabled = false;
                break;
        }
    }

    void SetText(bool value)
    {
        if (value)
        {
            text.text = "Yes";
        }
        else
        {
            text.text = "No";
        }
    }
}
