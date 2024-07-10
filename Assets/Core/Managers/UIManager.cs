using Assets.Core.Global;
using System.Collections;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class UIManager : MonoBehaviour
    {
        private CanvasGroup playerStatsScreen;
        private CanvasGroup botStatsScreen;

        private CanvasGroup botActionsScreen;

        private CanvasGroup playerWinScreen;
        private CanvasGroup playerLoseScreen;
        private CanvasGroup drawScreen;

        private CanvasGroup controlsGameplayScreen;
        private CanvasGroup controlsSystemScreen;
        private CanvasGroup fullScreenButton;

        public IEnumerator SetupUIManager(UIConfigData uiConfigData)
        {
            playerStatsScreen = uiConfigData.playerStatsScreen;
            botStatsScreen = uiConfigData.botStatsScreen;
            botActionsScreen = uiConfigData.botActionsScreen;
            playerWinScreen = uiConfigData.playerWinScreen;
            playerLoseScreen = uiConfigData.playerLoseScreen;
            drawScreen = uiConfigData.drawScreen;
            controlsGameplayScreen = uiConfigData.controlsGameplayScreen;
            controlsSystemScreen = uiConfigData.controlSystemScreen;
            fullScreenButton = uiConfigData.fullScreenButton;

            DisableAllPanels();

            GlobalEvents.Subscribe(GlobalEvents.ON_PLAYER_TAKES_TURN, ShowPlayerControls);
            GlobalEvents.Subscribe(GlobalEvents.ON_BOT_TAKES_TURN, ShowBotActionsScreen);
            GlobalEvents.Subscribe(GlobalEvents.ON_CARDS_REVEALED, ShowStats);
            GlobalEvents.Subscribe(GlobalEvents.ON_PLAYER_WIN, ShowPlayerWinScreen);
            GlobalEvents.Subscribe(GlobalEvents.ON_PLAYER_LOSE, ShowPlayerLoseScreen);
            GlobalEvents.Subscribe(GlobalEvents.ON_DRAW, ShowDrawScreen);
            GlobalEvents.Subscribe(GlobalEvents.ON_RESTART_TRIGGERED, HideControls);
            GlobalEvents.Subscribe(GlobalEvents.ON_CARDS_REVEALED, ShowFullScreenButton);

            yield return null;
        }
        public IEnumerator UninstalUIManager()
        {
            GlobalEvents.Unsubscribe(GlobalEvents.ON_PLAYER_TAKES_TURN, ShowPlayerControls);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_BOT_TAKES_TURN, ShowBotActionsScreen);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_CARDS_REVEALED, ShowStats);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_PLAYER_WIN, ShowPlayerWinScreen);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_PLAYER_LOSE, ShowPlayerLoseScreen);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_DRAW, ShowDrawScreen);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_RESTART_TRIGGERED, HideControls);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_CARDS_REVEALED, ShowFullScreenButton);

            yield return null;
        }

        private void ShowStats()
        {
            DisableAllPanels();
            playerStatsScreen.alpha = 1;
            botStatsScreen.alpha = 1;
        }
        private void ShowPlayerControls()
        {
            DisableAllPanels();
            controlsGameplayScreen.alpha = 1;
            controlsGameplayScreen.interactable = true;
            controlsGameplayScreen.blocksRaycasts = true;
        }
        private void ShowPlayerWinScreen()
        {
            DisableAllPanels();
            playerWinScreen.alpha = 1;
            ShowSystemControls();
        }
        private void ShowPlayerLoseScreen()
        {
            DisableAllPanels();
            playerLoseScreen.alpha = 1;
            ShowSystemControls();
        }
        private void ShowDrawScreen()
        {
            DisableAllPanels();
            drawScreen.alpha = 1;
            ShowSystemControls();
        }
        private void ShowBotActionsScreen()
        {
            DisableAllPanels();
            botActionsScreen.alpha = 1;
        }
        private void ShowSystemControls()
        {
            controlsSystemScreen.alpha = 1;
            controlsSystemScreen.interactable = true;
            controlsSystemScreen.blocksRaycasts = true;
        }
        private void ShowFullScreenButton()
        {
            fullScreenButton.alpha = 1;
            fullScreenButton.interactable = true;
            fullScreenButton.blocksRaycasts = true;
        }
        private void HideControls()
        {
            fullScreenButton.alpha = 0;
            controlsGameplayScreen.alpha = 0;
            controlsSystemScreen.alpha = 0;

            fullScreenButton.interactable = false;
            controlsGameplayScreen.interactable = false;
            controlsGameplayScreen.blocksRaycasts = false;

            fullScreenButton.blocksRaycasts = false;
            controlsSystemScreen.interactable = false;
            controlsSystemScreen.blocksRaycasts = false;
        }

        private void DisableAllPanels()
        {
            playerStatsScreen.alpha = 0;
            botStatsScreen.alpha = 0;
            botActionsScreen.alpha = 0;
            playerWinScreen.alpha = 0;
            playerLoseScreen.alpha = 0;
            drawScreen.alpha = 0;

            HideControls();
        }
    }
}