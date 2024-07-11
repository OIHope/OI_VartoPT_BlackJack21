using Assets.Core.Global;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class BotActionsLogManager : MonoBehaviour
    {
        private TextMeshProUGUI botActionsText;
        private string waitActionDescription;
        private string thinkActionDescription;
        private string takeCardActionDescription;
        private string passTurnActionDescription;

        public IEnumerator SetupBotActionsLogManager(UIConfigData uiConfigData)
        {
            botActionsText = uiConfigData.botActionsText;
            waitActionDescription = uiConfigData.waitActionDescription;
            thinkActionDescription = uiConfigData.thinkActionDescription;
            takeCardActionDescription = uiConfigData.takeCardActionDescription;
            passTurnActionDescription = uiConfigData.passTurnActionDescription;

            GlobalEvents.Subscribe(GlobalEvents.ON_BOT_WAITS, DisplayBotWaitAction);
            GlobalEvents.Subscribe(GlobalEvents.ON_BOT_THINKS, DisplayBotThinkAction);
            GlobalEvents.Subscribe(GlobalEvents.ON_BOT_TAKES_CARD, DisplayBotTakeCardAction);
            GlobalEvents.Subscribe(GlobalEvents.ON_BOT_PASS_TURN, DisplayBotPassTurnAction);

            yield return null;
        }
        public IEnumerator UninstalBotActionsLogManager()
        {
            GlobalEvents.Unsubscribe(GlobalEvents.ON_BOT_WAITS, DisplayBotWaitAction);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_BOT_THINKS, DisplayBotThinkAction);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_BOT_TAKES_CARD, DisplayBotTakeCardAction);
            GlobalEvents.Unsubscribe(GlobalEvents.ON_BOT_PASS_TURN, DisplayBotPassTurnAction);

            yield return null;
        }

        private void DisplayBotWaitAction()
        {
            botActionsText.text = waitActionDescription;
        }
        private void DisplayBotThinkAction()
        {
            botActionsText.text = thinkActionDescription;
        }
        private void DisplayBotTakeCardAction()
        {
            botActionsText.text = takeCardActionDescription;
        }
        private void DisplayBotPassTurnAction()
        {
            botActionsText.text = passTurnActionDescription;
        }
    }
}