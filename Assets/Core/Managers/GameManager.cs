using Assets.Core.Deck;
using Assets.Core.Global;
using Assets.Core.Hands;
using Assets.Core.PlayerContainer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("LoadScreen Related")]
        [Space]
        [SerializeField] private LoadingScreenManager loadingScreenManager;
        [Space(25)]
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
        [Header("TurnManager Related")]
        [Space]
        [SerializeField] private TurnManager turnManager;
        [Space]
        [SerializeField] private TurnConfigData turnConfigData;
        [SerializeField] private float lookAtScoreTime = 10f;
        [Space(25)]
        [Header("UI Related")]
        [Space]
        [SerializeField] private UIManager uiManager;
        [Space]
        [SerializeField] private UIConfigData uiConfigData;
        [Space(10)]
        [SerializeField] private UIControlsManager uiControlsManager;
        [SerializeField] private UIControlsData uiControlsData;



        private void Awake()
        {
            DisablePlayerControlls();
            StartCoroutine(SetupGame());
        }
        private IEnumerator SetupGame()
        {
            loadingScreenManager.FastLoadingScreen();

            yield return uiManager.SetupUIManager(uiConfigData);
            yield return uiControlsManager.SetupUIContolsManager(uiControlsData);

            yield return scoreManager.SetupScoreManager(scoreConfigData);
            yield return turnManager.SetupTurnManager(turnConfigData);

            yield return deckManager.SetupDeck(cardDeckToPlay);

            yield return loadingScreenManager.ToggleLoadingScreen(false);
            GlobalEvents.Subscribe(GlobalEvents.ON_RESTART_TRIGGERED, StartRestartingGame);

            yield return handsManager.SetupHandManager(playerHand, botHand, handConfigData);
            yield return turnManager.StartPlaying();

            yield return handsManager.RevealHands();

            GlobalEvents.Subscribe(GlobalEvents.ON_SKIP_BUTTON_CLICKED, SkipToSummingUp);

            yield return new WaitForSeconds(lookAtScoreTime);
            yield return scoreManager.SumUpScoreAndDeclareWinner();
        }
        private void SkipToSummingUp()
        {
            StopAllCoroutines();
            StartCoroutine(SumItUp());
        }
        private IEnumerator SumItUp()
        {
            GlobalEvents.Unsubscribe(GlobalEvents.ON_SKIP_BUTTON_CLICKED, SkipToSummingUp);
            yield return scoreManager.SumUpScoreAndDeclareWinner();
        }
        private void StartRestartingGame()
        {
            StopAllCoroutines();
            StartCoroutine(RestartGame());
        }
        private IEnumerator RestartGame()
        {
            GlobalEvents.Unsubscribe(GlobalEvents.ON_RESTART_TRIGGERED, StartRestartingGame);
            yield return loadingScreenManager.ToggleResettingScreen(true);

            yield return uiManager.UninstalUIManager();
            yield return uiControlsManager.UninstalUIControlsManager();
            yield return scoreManager.UninstalScoreManager();
            yield return handsManager.UninstalHandsManager();

            ReloadScene();
        }
        public void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        private void DisablePlayerControlls()
        {
            turnConfigData.takeCardButton.interactable = false;
            turnConfigData.finishTurnButton.interactable = false;
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
    public class TurnConfigData
    {
        public Player player;
        public Player bot;

        public Hand playerHand;
        public Hand botHand;

        public float playerThinkTime = 1f;
        public float botThinkTime = 2f;

        public Button takeCardButton;
        public Button finishTurnButton;
    }

    [System.Serializable]
    public class UIConfigData
    {
        public CanvasGroup playerStatsScreen;
        public CanvasGroup botStatsScreen;

        public CanvasGroup botActionsScreen;

        public CanvasGroup playerWinScreen;
        public CanvasGroup playerLoseScreen;
        public CanvasGroup drawScreen;

        public CanvasGroup controlsGameplayScreen;
        public CanvasGroup controlSystemScreen;
        public CanvasGroup fullScreenButton;
    }

    [System.Serializable]
    public class UIControlsData
    {
        public Button restartButton;
        public Button fullScreenButton;
    }
}