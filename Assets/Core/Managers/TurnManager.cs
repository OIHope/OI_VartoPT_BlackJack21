using Assets.Core.Hands;
using Assets.Core.PlayerContainer;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Managers
{
    public class TurnManager : MonoBehaviour
    {
        private Player player;
        private Player bot;

        private Hand playerHand;
        private Hand botHand;

        private PlayerControlls playerControlls = new();

        public IEnumerator SetupTurnManager(TurnConfigData turnConfigData)
        {
            player = turnConfigData.player;
            bot = turnConfigData.bot;

            playerHand = turnConfigData.playerHand;
            botHand = turnConfigData.botHand;

            playerControlls.playerThinkTime = turnConfigData.playerThinkTime;
            playerControlls.botThinkTime = turnConfigData.botThinkTime;

            playerControlls.takeCardButton = turnConfigData.takeCardButton;
            playerControlls.finishTurnButton = turnConfigData.finishTurnButton;

            yield return null;
        }

        public IEnumerator StartPlaying()
        {
            yield return TurnTakeCardsPhase();
            yield return TurnScoresCheckPhase();
        }
        private IEnumerator TurnTakeCardsPhase()
        {
            yield return Turn(player, playerHand);
            yield return new WaitForSeconds(playerControlls.playerThinkTime);

            yield return Turn(bot, botHand);
            yield return new WaitForSeconds(playerControlls.botThinkTime);
        }
        private IEnumerator TurnScoresCheckPhase()
        {
            yield return null;
        }
        private IEnumerator Turn(Player currentPlayer, Hand currentHand)
        {
            int maxScore = ScoreManager.Instance.MaxScore;
            yield return currentPlayer.StartTurn(currentHand, playerControlls, maxScore);
        }

    }
    [System.Serializable]
    public class PlayerControlls
    {
        public float playerThinkTime = 1f;
        public float botThinkTime = 2f;

        public Button takeCardButton;
        public Button finishTurnButton;
    }
}