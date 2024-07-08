using Assets.Core.Deck;
using Assets.Core.Hands;
using Assets.Core.PlayerContainer;
using System.Collections;
using TMPro;
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
        [Space(25)]
        [Header("ScoreManager Related")]
        [Space]
        [SerializeField] private ScoreManager scoreManager;
        [Space]
        [SerializeField] private ScoreConfigData scoreConfigData;
        [Space(25)]
        [Header("PlayersManager Related")]
        [Space]
        [SerializeField] private PlayerTurnManager playerTurnManager;
        [Space]
        [SerializeField] private PlayersConfigData playersConfigData;



        private void Awake()
        {
            StartCoroutine(SetupGame());
        }
        private IEnumerator SetupGame()
        {
            yield return scoreManager.SetupScoreManager(scoreConfigData);
            yield return playerTurnManager.SetupPlayerTurnManager(playersConfigData);

            yield return deckManager.SetupDeck(cardDeckToPlay);
            yield return handsManager.SetupHandManager(playerHand, botHand, handConfigData);

            yield return playerTurnManager.StartPlaying();
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

    [System.Serializable]
    public class ScoreConfigData
    {
        public int maxScoreToWin = 21;
        [Space]
        public TextMeshProUGUI uiPlayerScoreText;
        public TextMeshProUGUI uiBotScoreText;
    }

    [System.Serializable]
    public class PlayersConfigData
    {
        public Player player;
        public Player bot;
    }
}