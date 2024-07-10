using Assets.Core.Global;
using Assets.Core.Hands;
using Assets.Core.Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Core.PlayerContainer
{
    public class BotControl : Player
    {
        public override IEnumerator StartTurn(Hand playersHand, PlayerControlls playerControlls, int maxScore)
        {
            GlobalEvents.InvokeEvent(GlobalEvents.ON_BOT_WAITS);
            yield return base.StartTurn(playersHand, playerControlls, maxScore);
            thinkTime = playerControlls.botThinkTime;

            while (!readyToPassTurn)
            {
                GlobalEvents.InvokeEvent(GlobalEvents.ON_BOT_THINKS);
                yield return new WaitForSeconds(thinkTime);

                if (WillTakeCard(score, maxScore))
                {
                    GlobalEvents.InvokeEvent(GlobalEvents.ON_BOT_TAKES_CARD);
                    TakeCard();
                }
                else
                {
                    GlobalEvents.InvokeEvent(GlobalEvents.ON_BOT_PASS_TURN);
                    FinishTurn();
                }
            }

            yield return new WaitForSeconds(thinkTime);
        }

        protected override void TakeCard()
        {
            GlobalEvents.InvokeEvent(GlobalEvents.ON_PLAYER_TAKES_CARD, playersHand, false);
        }

        protected override void UpdateScore(Hand currentHand, int score)
        {
            if (!currentHand.IsPlayersHand)
            {
                this.score = score;
            }
            if (this.score > ScoreManager.Instance.MaxScore)
            {
                FinishTurn();
            }
        }

        private bool WillTakeCard(int currentScore, int maxScore)
        {
            float scoreProbability = (float)currentScore / (float)maxScore;
            float randomValue = Random.Range(0.3f, 0.9f);

            bool willTakeCard = randomValue > scoreProbability;

            return willTakeCard;
        }
    }
}