using MainMenuEnum;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldforSettings : MonoBehaviour
{
    [SerializeField] TargetValue targetValue;

    private void Awake()
    {
        GetComponent<InputField>().onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(string arg0)
    {
        switch (targetValue)
        {
            case TargetValue.OwnerName:
                if (arg0 == string.Empty)
                {
                    ScreenshotSystemSettings.Instance.OwnerName = "default";
                }
                else
                {
                    ScreenshotSystemSettings.Instance.OwnerName = arg0;
                }
                break;
            case TargetValue.timeDelay:
                if (arg0 == string.Empty)
                {
                    ScreenshotSystemSettings.Instance.TimeDelay = 0.5f;
                }
                else
                {
                    float timeValue = float.Parse(arg0);
                    ScreenshotSystemSettings.Instance.TimeDelay = timeValue;
                }
                break;
            case TargetValue.GridAccuracy:
                if (arg0 == string.Empty)
                {
                    ScreenshotSystemSettings.Instance.GridAccuracy = 10;
                }
                else
                {
                    int gridValue = int.Parse(arg0);
                    ScreenshotSystemSettings.Instance.GridAccuracy = gridValue;
                }
                break;
            default:
                Debug.LogError("Missing or wrong targetValue in:" + transform.parent.parent.name);
                this.enabled = false;
                break;
        }
    }
}
