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
            this.playerHand = playerHand;
            this.botHand = botHand;
            this.deck = handConfigData.deckManager;

            playerHand.SetupHand(handConfigData);
            botHand.SetupHand(handConfigData);

            int takeCardsCount = handConfigData.startWithCardsCount;
            for (int i = 0; i < takeCardsCount; i++)
            {
                yield return HandTakeCard(playerHand, true);
                yield return HandTakeCard(botHand, false);
            }

            yield return null;
        }
        public IEnumerator HandTakeCard(Hand hand, bool openCard)
        {
            CardData cardData = deck.TakeCardFromDeck();
            hand.AddCard(cardData, openCard);
            yield return new WaitForSeconds(takeCardsDelay);
        }
    }
}