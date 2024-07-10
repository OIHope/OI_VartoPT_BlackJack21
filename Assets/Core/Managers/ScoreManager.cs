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
            this.uiPlayerScoreText = scoreConfigData.uiPlayerScoreText;
            this.uiBotScoreText = scoreConfigData.uiBotScoreText;
            this.maxScoreToWin = scoreConfigData.maxScoreToWin;

            GlobalEvents.Subscribe<Hand>(GlobalEvents.ON_CARD_IS_TAKEN, CalculateScore);

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

            bool winningScore = handScore == maxScoreToWin;

            if (currentHand.IsPlayersHand)
            {
                DisplayScore(uiPlayerScoreText, ("Player"), handScore, overflow, winningScore);
            }
            else
            {
                DisplayScore(uiBotScoreText, ("Bot"), handScore, overflow, winningScore);
            }

            GlobalEvents.InvokeEvent(GlobalEvents.ON_SCORE_UPDATED, currentHand, handScore);
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

        private void DisplayScore(TextMeshProUGUI uiText, string playerName, int scoreValue, bool overflow, bool winningScore)
        {
            string name = playerName;
            string inBetween = ("'s score: ");
            string score = scoreValue.ToString();

            string fullDescription = name + inBetween + score;

            if (overflow)
            {
                fullDescription = name + (" fucked up!");
            }
            if (winningScore)
            {
                fullDescription = name.ToUpper() + (" SCORED!");
            }

            uiText.text = fullDescription;
        }
    }
}