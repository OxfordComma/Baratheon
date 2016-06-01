using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayHandler : MonoBehaviour
{
    Player currentPlayer;
    Deck playerDeck;
    public GameObject cardUIPrefab;
    public GameObject handWindow;


    // Use this for initialization
    void Start()
    {
        currentPlayer = GameObject.Find("Player Handler").GetComponent<PlayerHandler>().currentPlayer;
        playerDeck = currentPlayer.deck;

        playerDeck.Shuffle();


        for (int i = 0; i < 7; i++)
        {
            Draw();            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw()
    {
        Card card = playerDeck.cards[0];
        playerDeck.cards.RemoveAt(0);
        GameObject cardUIToAdd = Instantiate(cardUIPrefab);
        cardUIToAdd.GetComponent<CardUI>().SetCard(card);
        cardUIToAdd.transform.SetParent(handWindow.transform);
    }
}
