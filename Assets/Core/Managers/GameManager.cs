using Assets.Core.Deck;
using Assets.Core.Hands;
using System.Collections;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Deck Related")]
        [Space]
        [SerializeField] private CardDeck cardDeckToPlay;
        [SerializeField] private DeckManager deckManager;
        [Space(25)]
        [Header("Hands Related")]
        [Space]
        [SerializeField] private HandsManager handsManager;
        [Space]
        [SerializeField] private Hand playerHand;
        [SerializeField] private Hand botHand;
        [Space]
        [SerializeField] private HandConfigData handConfigData;


        private void Awake()
        {
            StartCoroutine(SetupGame());
        }
        private IEnumerator SetupGame()
        {
            yield return deckManager.SetupDeck(cardDeckToPlay);
            yield return handsManager.SetupHandManager(playerHand, botHand, handConfigData);

            yield return null;
        }
    }

    [System.Serializable]
    public class HandConfigData
    {
        public Transform deckTransform;
        public DeckManager deckManager;
        [Space]
        public GameObject cardPrefab;
        public int startWithCardsCount = 2;
        public int maxCardsInHand = 11;
        [Space]
        public float maxSpacing = 1f;
        public float minSpacing = 0.25f;
        public int maxNoOverlapCards = 2;
        [Space]
        public int initialRenderingOrder = 500;
        [Space(25)]
        public AnimationCurve transitionSpeedCurve;
    }
}