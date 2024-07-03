using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform deckTransform;
    [SerializeField] private float maxSpacing = 1f;
    [SerializeField] private float minSpacing = 0.25f;
    [SerializeField] private int maxNoOverlapCards = 2;
    [SerializeField] private int maxCardsInHand = 11;
    [SerializeField] private int initialRenderingOrder = 500;

    [SerializeField] private AnimationCurve transitionSpeedCurve;

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
        int cardCount = handTransform.childCount;

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
            startPositions[i] = handTransform.GetChild(i).localPosition;
        }

        float transitionDuration = GetAnimationCurveDuration(transitionSpeedCurve);

        while (elapsedTime < transitionDuration)
        {
            for (int i = 0; i < cardCount; i++)
            {
                Transform card = handTransform.GetChild(i);
                card.localPosition = Vector3.Lerp(startPositions[i], targetPositions[i], transitionSpeedCurve.Evaluate(elapsedTime));
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = initialRenderingOrder; i < cardCount; i++)
        {
            Transform card = handTransform.GetChild(i);
            card.localPosition = targetPositions[i];

            // Встановлюємо порядок рендерингу
            SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = i;
            }
        }
    }

    [ContextMenu("Add card")]
    public void AddCard()
    {
        Instantiate(cardPrefab, deckTransform.position, Quaternion.identity, handTransform);
        UpdateCardPositions();
    }

    public void RemoveCard(GameObject card)
    {
        Destroy(card);
        UpdateCardPositions();
    }
}
