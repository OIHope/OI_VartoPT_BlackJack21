using System;
using TMPro;
using UnityEngine;

namespace Assets.Core.Cards
{
    public class Card : MonoBehaviour
    {
        [Header("Card Visuals")]
        [Space]
        [Tooltip("Main sprite that will be changed from CardData")]
        [SerializeField] private SpriteRenderer cardSpriteRenderer;

        [Space(25)]
        [Header("Debug Only")]
        [Space]
        [SerializeField] private CardData cardData;


        public void ChangeCardRenderingOrder(int order)
        {
            cardSpriteRenderer.sortingOrder = order;
        }

        public void SetupCard(CardData cardData, bool openThisCard)
        {
            this.cardData = cardData;
            if (openThisCard)
            {
                ToggleCardOpen(true);
            }
            else
            {
                ToggleCardOpen(false);
            }
        }
        public int CardValue(bool overflow)
        {
            return cardData.GetCardValue(overflow);
        }

        public void ToggleCardOpen(bool cardMustBeOpened)
        {
            if (cardData == null) return;

            if (cardMustBeOpened)
            {
                ChangeCardSprite(cardData.CardFrontSprite);
                return;
            }
            ChangeCardSprite(cardData.CardBackSprite);
        }

        private void ChangeCardSprite(Sprite sprite)
        {
            cardSpriteRenderer.sprite = sprite;
        }
        
    }
}