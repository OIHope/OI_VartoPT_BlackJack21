using Assets.Core.Global;
using Assets.Core.Hands;
using Assets.Core.Managers;
using System.Collections;

namespace Assets.Core.PlayerContainer
{
    public class BotControl : Player
    {
        public override IEnumerator StartTurn(Hand playersHand, PlayerControlls playerControlls)
        {
            thinkTime = playerControlls.botThinkTime;

            yield return null;
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
        }
    }
}