using Assets.Core.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core.Deck
{
    [CreateAssetMenu(menuName =("Deck"))]
    public class CardDeck : ScriptableObject
    {
        [SerializeField] private List<CardData> cardsDataList;

        public List<CardData> GetShuffledCardDeck()
        {
            List<CardData> shuffledDeck = new(cardsDataList);
            for (int i = 0; i < shuffledDeck.Count; i++)
            {
                int randomID = Random.Range(i, shuffledDeck.Count);
                (shuffledDeck[randomID], shuffledDeck[i]) = (shuffledDeck[i], shuffledDeck[randomID]);
            }
            return shuffledDeck;
        }
    }
}