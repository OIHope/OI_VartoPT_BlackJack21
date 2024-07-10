using Assets.Core.Global;
using Assets.Core.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIControlsManager : MonoBehaviour
{
    private Button restartButton;
    private Button fullScreenButton;

    public IEnumerator SetupUIContolsManager(UIControlsData uiControlsData)
    {
        restartButton = uiControlsData.restartButton;
        fullScreenButton = uiControlsData.fullScreenButton;

        restartButton.onClick.AddListener(OnRestartButtonClicked);
        fullScreenButton.onClick.AddListener(GetToResult);


        yield return null;
    }
    public IEnumerator UninstalUIControlsManager()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        fullScreenButton.onClick.RemoveListener(GetToResult);

        yield return null;
    }
    private void OnRestartButtonClicked()
    {
        GlobalEvents.InvokeEvent(GlobalEvents.ON_RESTART_TRIGGERED);
    }
    private void GetToResult()
    {
        GlobalEvents.InvokeEvent(GlobalEvents.ON_SKIP_BUTTON_CLICKED);
    }
}
