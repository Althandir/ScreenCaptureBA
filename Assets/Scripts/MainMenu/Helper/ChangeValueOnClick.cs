using MainMenuEnum;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeValueOnClick : MonoBehaviour
{
    [SerializeField] TargetValue targetValue;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        switch (targetValue)
        {
            case TargetValue.heatmap:
                if (ScreenshotSystemSettings.Instance.CatchHeatmap)
                {
                    ScreenshotSystemSettings.Instance.CatchHeatmap = false;
                }
                else
                {
                    ScreenshotSystemSettings.Instance.CatchHeatmap = true;
                }
                break;
            case TargetValue.pupildata:
                if (ScreenshotSystemSettings.Instance.CatchPupils)
                {
                    ScreenshotSystemSettings.Instance.CatchPupils = false;
                }
                else
                {
                    ScreenshotSystemSettings.Instance.CatchPupils = true;
                }
                break;
            default:
                Debug.LogError("Missing targetValue in:" + transform.parent.parent.name + ":" + transform.parent.name); ;
                this.enabled = false;
                break;
        }
    }
}
