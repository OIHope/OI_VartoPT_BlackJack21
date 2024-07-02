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
        [SerializeField] private Sprite cardSprite;
        [Space]
        [Tooltip("Array of TMP taht will display cards value from CardData")]
        [SerializeField] private TextMeshPro[] cardTextArray;

        [Space(25)]
        [Header("Debug Only")]
        [Space]
        [SerializeField] private CardData cardData;
        [SerializeField] private bool cardIsOpen = false;

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
                cardIsOpen = true;
                ChangeCardSprite(cardData.CardFrontSprite);
                DisplayCardText(true);

                return;
            }

            cardIsOpen = false;
            ChangeCardSprite(cardData.CardBackSprite);
            DisplayCardText(false);
        }

        private void DisplayCardText(bool display)
        {
            if (!display)
            {
                foreach(TextMeshPro cardText in cardTextArray)
                {
                    cardText.gameObject.SetActive(false);
                }
                return;
            }
            foreach (TextMeshPro cardText in cardTextArray)
            {
                cardText.gameObject.SetActive(true);
                cardText.text = cardData.GetCardDisplayInfo();
            }
        }
        private void ChangeCardSprite(Sprite sprite)
        {
            cardSprite = sprite;
        }
    }
}