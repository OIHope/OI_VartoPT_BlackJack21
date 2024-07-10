using UnityEngine;
using System.Collections;
using Assets.Core.Cards;
using Assets.Core.Managers;
using System.Collections.Generic;
using Assets.Core.Global;

namespace Assets.Core.Hands
{
    public class Hand : MonoBehaviour
    {
        private GameObject cardPrefab;
        private Transform deckTransform;
        private float maxSpacing = 1f;
        private float minSpacing = 0.25f;
        private int maxNoOverlapCards = 2;
        private int maxCardsInHand = 11;
        private int initialRenderingOrder = 500;
        private AnimationCurve transitionSpeedCurve;

        private List<Card> cardList = new();
        public List<Card> CardsOnHand => cardList;

        private bool playersHand = false;
        public bool IsPlayersHand => playersHand;

        public void SetupHand(HandConfigData handConfigData, bool isPlayersHand)
        {
            CleanHand();

            cardPrefab = handConfigData.cardPrefab;
            deckTransform = handConfigData.deckTransform;
            maxSpacing = handConfigData.maxSpacing;
            minSpacing = handConfigData.minSpacing;
            maxNoOverlapCards = handConfigData.maxNoOverlapCards;
            maxCardsInHand = handConfigData.maxCardsInHand;
            initialRenderingOrder = handConfigData.initialRenderingOrder;
            transitionSpeedCurve = handConfigData.transitionSpeedCurve;

            playersHand = isPlayersHand;
        }

        private float GetAnimationCurveDuration(AnimationCurve animationCurve)
        {
            Keyframe maxTimeKeyframe = animationCurve.keys[0];
            foreach (Keyframe key in animationCurve.keys)
            {
                if (key.time > maxTimeKeyframe.time) maxTimeKeyframe = key;
            }
            return maxTimeKeyframe.time;
        }
        private void UpdateCardPositions()
        {
            StartCoroutine(UpdateCardPositionsCoroutine());
        }

        private IEnumerator UpdateCardPositionsCoroutine()
        {
            int cardCount = transform.childCount;

            for (int i = 0; i < cardCount; i++)
            {
                Transform cardTransform = transform.GetChild(i);
                Card card = cardTransform.GetComponent<Card>();
                card.ChangeCardRenderingOrder(i + initialRenderingOrder);
            }

            float cardSpacing;
            if (cardCount <= maxNoOverlapCards)
            {
                cardSpacing = maxSpacing;
            }
            else
            {
                cardSpacing = Mathf.Lerp(maxSpacing, minSpacing, (float)(cardCount - maxNoOverlapCards) / (maxCardsInHand - maxNoOverlapCards));
            }

            float handWidth = cardSpacing * (cardCount - 1);
            float startX = -handWidth / 2;

            Vector3[] targetPositions = new Vector3[cardCount];
            for (int i = 0; i < cardCount; i++)
            {
                float xPos = startX + i * cardSpacing;
                targetPositions[i] = new Vector3(xPos, 0, 0);
            }

            float elapsedTime = 0f;
            Vector3[] startPositions = new Vector3[cardCount];
            for (int i = 0; i < cardCount; i++)
            {
                startPositions[i] = transform.GetChild(i).localPosition;
            }

            float transitionDuration = GetAnimationCurveDuration(transitionSpeedCurve);

            while (elapsedTime < transitionDuration)
            {
                for (int i = 0; i < cardCount; i++)
                {
                    Transform cardTransform = transform.GetChild(i);
                    cardTransform.localPosition = Vector3.Lerp(startPositions[i], targetPositions[i], transitionSpeedCurve.Evaluate(elapsedTime));
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public void AddCard(CardData cardData, bool openCard)
        {
            GameObject newCard = Instantiate(cardPrefab, deckTransform.position, Quaternion.identity, transform);

            Card card = newCard.transform.GetComponent<Card>();
            card.SetupCard(cardData, openCard);
            cardList.Add(card);

            GlobalEvents.InvokeEvent<Hand>(GlobalEvents.ON_CARD_IS_TAKEN, this);
            UpdateCardPositions();
        }

        public IEnumerator OpenHand()
        {
            foreach (Card card in cardList)
            {
                card.ToggleCardOpen(true);
            }

            yield return MoveCards();
        }
        private IEnumerator MoveCards()
        {
            int cardCount = cardList.Count;
            float yDisplaysment = playersHand ? 0.3f : -0.3f;

            Vector3[] startPositions = new Vector3[cardCount];
            for (int i = 0; i < cardCount; i++)
            {
                startPositions[i] = transform.GetChild(i).localPosition;
            }

            Vector3[] targetPositions = new Vector3[cardCount];
            for (int i = 0; i < cardCount; i++)
            {
                targetPositions[i] = startPositions[i];
                targetPositions[i].y += yDisplaysment;
            }
            float elapsedTime = 0f;
            float transitionDuration = GetAnimationCurveDuration(transitionSpeedCurve);

            while (elapsedTime < transitionDuration)
            {
                for (int i = 0; i < cardCount; i++)
                {
                    Transform cardTransform = transform.GetChild(i);
                    cardTransform.localPosition = Vector3.Lerp(startPositions[i], targetPositions[i], transitionSpeedCurve.Evaluate(elapsedTime));
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public void CleanHand()
        {
            foreach(Transform cardTransform in transform)
            {
                Destroy(cardTransform.gameObject);
            }
        }
    }
}