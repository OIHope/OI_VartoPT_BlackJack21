using Assets.Core.Global;
using Assets.Core.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIControlsManager : MonoBehaviour
{
    private Button restartButton;

    public IEnumerator SetupUIContolsManager(UIControlsData uiControlsData)
    {
        restartButton = uiControlsData.restartButton;
        restartButton.onClick.AddListener(OnRestartButtonClicked);

        yield return null;
    }
    public IEnumerator UninstalUIControlsManager()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        yield return null;
    }
    private void OnRestartButtonClicked()
    {
        GlobalEvents.InvokeEvent(GlobalEvents.ON_RESTART_TRIGGERED);
    }
}
