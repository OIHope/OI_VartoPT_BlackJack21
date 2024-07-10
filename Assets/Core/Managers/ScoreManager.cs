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
        private TextMeshProUGUI uiPlayerScoreText;
        private TextMeshProUGUI uiBotScoreText;

        private int maxScoreToWin;

        private int _playerScore;
        private int _botScore;

        public int PlayerScore => _playerScore;
        public int BotScore => _botScore;

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
            if (handScore > maxScoreToWin)
            {
                handScore = CulculateCardsValues(cardsOnHandList, true);
            }

            if (currentHand.IsPlayersHand)
            {
                DisplayScore(uiPlayerScoreText, handScore);
            }
            else
            {
                DisplayScore(uiBotScoreText, handScore);
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

        private void DisplayScore(TextMeshProUGUI uiText, int scoreValue)
        {
            string introduction = ("Score: ");
            string score = scoreValue.ToString();

            uiText.text = introduction + score;
        }
    }
}