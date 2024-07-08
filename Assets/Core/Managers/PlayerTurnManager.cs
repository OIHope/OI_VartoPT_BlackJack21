using Assets.Core.PlayerContainer;
using System.Collections;
using UnityEngine;

namespace Assets.Core.Managers
{
    public class PlayerTurnManager : MonoBehaviour
    {
        private Player player;
        private Player bot;

        bool playerTurnIsOver = false;
        bool botTurnIsOver = false;

        public IEnumerator SetupPlayerTurnManager(PlayersConfigData playersConfigData)
        {
            playerTurnIsOver = false;
            botTurnIsOver = false;

            player = playersConfigData.player;
            bot = playersConfigData.bot;

            yield return null;
        }

        public IEnumerator StartPlaying()
        {
            StartTurn(player);
            yield return new WaitUntil(() => playerTurnIsOver);

            StartTurn(bot);
            yield return new WaitUntil(() => botTurnIsOver);

            yield return null;
        }

        private void StartTurn(Player currentPlayer)
        {
            StartCoroutine(Turn(currentPlayer));
        }
        private IEnumerator Turn(Player currentPlayer)
        {
            yield return null;
        }

    }
}