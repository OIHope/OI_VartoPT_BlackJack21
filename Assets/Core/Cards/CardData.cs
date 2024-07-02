using UnityEngine;

namespace Assets.Core.Cards
{
    [CreateAssetMenu(menuName = ("Card"))]
    public class CardData : ScriptableObject
    {
        [Header("Card Values:")]
        [Space]
        [Tooltip("This gives the card a single value")]
        [SerializeField][Range(2,11)] private int cardValue = 2;
        [Space]
        [Tooltip("Defies if card has two values:")]
        [SerializeField] private bool hasTwoValues = false;
        [SerializeField][Range(1, 5)] private int firstCardValue = 1;
        [SerializeField][Range(6, 11)] private int secondCardValue = 11;
        [Space(25)]
        [Header("Card Visual:")]
        [Space]
        [SerializeField] private Sprite frontSprite;
        [SerializeField] private Sprite backSprite;

        public Sprite CardFrontSprite => frontSprite;
        public Sprite CardBackSprite => backSprite;

        public int GetCardValue(bool overflow)
        {
            if (overflow && hasTwoValues)
            {
                return firstCardValue;
            }
            else if (!overflow && hasTwoValues)
            {
                return secondCardValue;
            }
            else
            {
                return cardValue;
            }
        }
        public string GetCardDisplayInfo()
        {
            string cardInfo;

            if (hasTwoValues)
            {
                cardInfo = ($"+ {firstCardValue} / + {secondCardValue}");
                return cardInfo;
            }
            cardInfo = ($"+ {cardValue}");
            return cardInfo;
        }
    }
}