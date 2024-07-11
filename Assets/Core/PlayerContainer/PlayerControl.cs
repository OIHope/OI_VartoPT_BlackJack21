using Assets.Core.Global;
using Assets.Core.Hands;
using Assets.Core.Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Core.PlayerContainer
{
    public class PlayerControl : Player
    {
        public override IEnumerator StartTurn(Hand playersHand, PlayerControlls playerControlls, int maxScore)
        {
            yield return base.StartTurn(playersHand, playerControlls, maxScore);

            playerControlls.takeCardButton.onClick.AddListener(TakeCard);
            playerControlls.finishTurnButton.onClick.AddListener(FinishTurn);

            TogglePlayerControls(playerControlls, true);

            this.playersHand = playersHand;
            thinkTime = playerControlls.playerThinkTime;

            yield return new WaitUntil(() => readyToPassTurn);
            SoundManager.Instance.PlaySoundFX(turnReactionsPassTurnSFX, transform, 1f, false);

            playerControlls.takeCardButton.onClick.RemoveListener(TakeCard);
            playerControlls.finishTurnButton.onClick.RemoveListener(FinishTurn);

            TogglePlayerControls(playerControlls, false);
        }

        private static void TogglePlayerControls(PlayerControlls playerControlls, bool enable)
        {
            playerControlls.takeCardButton.interactable = enable;
            playerControlls.finishTurnButton.interactable = enable;
        }

        protected override void TakeCard()
        {
            GlobalEvents.InvokeEvent(GlobalEvents.ON_PLAYER_TAKES_CARD, playersHand, true);
            SoundManager.Instance.PlaySoundFX(turnReactionsTakeCardSFX, transform, 1f, false);
        }

        protected override void UpdateScore(Hand currentHand, int score)
        {
            if (currentHand.IsPlayersHand)
            {
                this.score = score;
            }
            if (this.score >= ScoreManager.Instance.MaxScore)
            {
                FinishTurn();
            }
        }
    }
}