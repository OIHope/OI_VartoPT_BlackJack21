using Assets.Core.Deck;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Deck Related")]
    [Space]
    [SerializeField] private CardDeck cardDeckToPlay;
    [SerializeField] private Deck deck;


    private void Awake()
    {
        StartCoroutine(SetupGame());
    }
    private IEnumerator SetupGame()
    {
        yield return deck.SetupDeck(cardDeckToPlay);


        yield return null;
    }
}
