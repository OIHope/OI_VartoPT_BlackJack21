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
        protected float thinkTime = 1f;

        protected int score;
        protected bool subscribedToScoreUpdate = false;

        protected bool readyToPassTurn = false;

        protected void Awake()
        {
            if (!subscribedToScoreUpdate)
            {
                SubscribeToScoreUpdate(true);
            }
        }

        public virtual IEnumerator StartTurn(Hand playersHand, PlayerControlls playerControlls, int maxScore)
        {
            if (!subscribedToScoreUpdate)
            {
                SubscribeToScoreUpdate(true);
            }
            this.playersHand = playersHand;
            readyToPassTurn = false;
            yield return null;
        }
        protected abstract void TakeCard();
        protected abstract void UpdateScore(Hand currentHand, int score);

        protected void FinishTurn()
        {
            if (subscribedToScoreUpdate)
            {
                SubscribeToScoreUpdate(false);
            }
            readyToPassTurn = true;
        }
        protected void SubscribeToScoreUpdate(bool subscribe)
        {
            if (subscribe)
            {
                GlobalEvents.Subscribe<Hand, int>(GlobalEvents.ON_SCORE_UPDATED, UpdateScore);
                subscribedToScoreUpdate = subscribe;
            }
            else
            {
                GlobalEvents.Unsubscribe<Hand, int>(GlobalEvents.ON_SCORE_UPDATED, UpdateScore);
                subscribedToScoreUpdate = subscribe;
            }
        }
    }
}