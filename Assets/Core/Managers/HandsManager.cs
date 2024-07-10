using Assets.Core.Cards;
using Assets.Core.Global;
using Assets.Core.Hands;
using System.Collections;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class HandsManager : MonoBehaviour
    {
        [SerializeField][Range(0.1f, 1f)] private float takeCardsDelay = 0.5f;
        
        private Hand playerHand;
        private Hand botHand;

        private DeckManager deck;

        public IEnumerator SetupHandManager(Hand playerHand, Hand botHand, HandConfigData handConfigData)
        {
            GlobalEvents.Subscribe<Hand, bool>(GlobalEvents.ON_PLAYER_TAKES_CARD, TakeCard);

            this.playerHand = playerHand;
            this.botHand = botHand;
            deck = handConfigData.deckManager;

            playerHand.SetupHand(handConfigData, true);
            botHand.SetupHand(handConfigData, false);

            int takeCardsCount = handConfigData.startWithCardsCount;
            for (int i = 0; i < takeCardsCount; i++)
            {
                yield return HandTakesCard(playerHand, true);
                yield return HandTakesCard(botHand, false);
            }

            yield return null;
        }
        private void TakeCard(Hand hand, bool openCard)
        {
            StartCoroutine(HandTakesCard(hand, openCard));
        }
        public IEnumerator HandTakesCard(Hand hand, bool openCard)
        {
            CardData cardData = deck.TakeCardFromDeck();
            hand.AddCard(cardData, openCard);
            yield return new WaitForSeconds(takeCardsDelay);
        }
    }
}