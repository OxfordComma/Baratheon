using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayHandler : MonoBehaviour
{
    Player currentPlayer;
    Deck playerDeck;
    GameObject cardUIPrefab, contextMenuPrefab;
    public GameObject handWindow;
    Transform zoomedCard = null;

    Player whosTurnIsIt;

    // Use this for initialization
    void Start()
    {
		cardUIPrefab = Resources.Load ("Prefabs/CardUI") as GameObject;
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
		cardUIToAdd.transform.SetParent(handWindow.transform, false);
    }

    
}
