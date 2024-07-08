using Assets.Core.Cards;
using Assets.Core.Deck;
using Assets.Core.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class DeckManager : MonoBehaviour
    {
        [Header("Debug Only!")]
        [SerializeField] private CardDeck cardDeck;
        [SerializeField] private List<CardData> cardDataList;

        public IEnumerator SetupDeck(CardDeck deckToPlay)
        {
            cardDeck = deckToPlay;
            cardDataList = cardDeck.GetShuffledCardDeck();
            yield return null;

        }

        public CardData TakeCardFromDeck()
        {
            if (cardDataList == null || cardDataList.Count == 0)
            {
                throw new Exception("Deck is empty!");
            }

            CardData cardToGive = cardDataList[0];
            cardDataList.RemoveAt(0);
            return cardToGive;
        }

    }
}