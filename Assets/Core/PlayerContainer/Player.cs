using Assets.Core.Global;
using Assets.Core.Hands;
using Assets.Core.Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Core.PlayerContainer
{
    public abstract class Player : MonoBehaviour
    {
        protected Hand playersHand;
        protected int score;
        protected float thinkTime = 1f;
        protected bool readyToPassTurn = false;

        public virtual IEnumerator StartTurn(Hand playersHand, PlayerControlls playerControlls)
        {
            SubscribeToScoreUpdate(true);
            this.playersHand = playersHand;
            yield return null;
        }
        protected abstract void TakeCard();
        protected abstract void UpdateScore(Hand currentHand, int score);

        protected void FinishTurn()
        {
            SubscribeToScoreUpdate(false);
            readyToPassTurn = true;
        }
        protected void SubscribeToScoreUpdate(bool subscribe)
        {
            if (subscribe)
            {
                GlobalEvents.Subscribe<Hand, int>(GlobalEvents.ON_SCORE_UPDATED, UpdateScore);
            }
            else
            {
                GlobalEvents.Unsubscribe<Hand, int>(GlobalEvents.ON_SCORE_UPDATED, UpdateScore);
            }
        }
    }
}