using Assets.Core.Cards;
using Assets.Core.Global;
using Assets.Core.Hands;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager _instance;
        public static ScoreManager Instance { get { return _instance; } }


        private TextMeshProUGUI uiPlayerScoreText;
        private TextMeshProUGUI uiBotScoreText;

        private int maxScoreToWin;

        private int _playerScore;
        private int _botScore;

        public int PlayerScore => _playerScore;
        public int BotScore => _botScore;
        public int MaxScore => maxScoreToWin;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public IEnumerator SetupScoreManager(ScoreConfigData scoreConfigData)
        {
            uiPlayerScoreText = scoreConfigData.uiPlayerScoreText;
            uiBotScoreText = scoreConfigData.uiBotScoreText;
            maxScoreToWin = scoreConfigData.maxScoreToWin;

            GlobalEvents.Subscribe<Hand>(GlobalEvents.ON_CARD_IS_TAKEN, CalculateScore);

            yield return null;
        }
        public IEnumerator UninstalScoreManager()
        {
            GlobalEvents.Unsubscribe<Hand>(GlobalEvents.ON_CARD_IS_TAKEN, CalculateScore);

            yield return null;
        }

        private void CalculateScore(Hand currentHand)
        {
            List<Card> cardsOnHandList = currentHand.CardsOnHand;

            int handScore = CulculateCardsValues(cardsOnHandList, false);

            bool overflow = handScore > maxScoreToWin;
            if (overflow)
            {
                handScore = CulculateCardsValues(cardsOnHandList, true);
            }

            if (currentHand.IsPlayersHand)
            {
                _playerScore = handScore;
                DisplayScore(uiPlayerScoreText, ("Player"), handScore);
            }
            else
            {
                _botScore = handScore;
                DisplayScore(uiBotScoreText, ("Bot"), handScore);
            }

            GlobalEvents.InvokeEvent(GlobalEvents.ON_SCORE_UPDATED, currentHand, handScore);
        }

        public IEnumerator SumUpScoreAndDeclareWinner()
        {
            bool playerBust = _playerScore > maxScoreToWin;
            bool botBust = _botScore > maxScoreToWin;

            bool playerWinsCondition = !playerBust && (botBust || _playerScore > _botScore);
            bool botWinsCondition = !botBust && (playerBust || _botScore > _playerScore);
            bool drawCondition = !playerBust && !botBust && _playerScore == _botScore;

            yield return new WaitForSeconds(0.1f);

            if (playerWinsCondition)
            {
                GlobalEvents.InvokeEvent(GlobalEvents.ON_PLAYER_WIN);
            }
            else if (botWinsCondition)
            {
                GlobalEvents.InvokeEvent(GlobalEvents.ON_PLAYER_LOSE);
            }
            else if (drawCondition)
            {
                GlobalEvents.InvokeEvent(GlobalEvents.ON_DRAW);
            }
            else
            {
                Debug.Log("Unexpected condition: no winner and no draw!");
            }
        }

        private int CulculateCardsValues(List<Card> cardsOnHandList, bool overflow)
        {
            int cardsCount = cardsOnHandList.Count;
            int handScore = 0;

            for (int i = 0; i < cardsCount; i++)
            {
                int cardScore = cardsOnHandList[i].CardValue(overflow);
                handScore += cardScore;
            }

            return handScore;
        }

        private void DisplayScore(TextMeshProUGUI uiText, string playerName, int scoreValue)
        {
            string name = playerName;
            string inBetween = ("'s score: ");
            string score = scoreValue.ToString();

            string fullDescription = name + inBetween + score;
            uiText.text = fullDescription;
        }
    }
}