using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class GameFunctionsHandler
{
    public Transform GetCardUnderMouse()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 mousePos = Input.mousePosition;
        Debug.DrawLine(mousePos, Vector2.zero, Color.cyan);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
        return hit.transform;
    }

    public void Draw(Player player, int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            Deck playerDeck = player.deck;
            Card topCard = playerDeck.cards[0];

            GameObject cardInHandObject = GameObject.Instantiate(Resources.Load("Prefabs/Battlefield/CardInHand")) as GameObject;
            cardInHandObject.GetComponent<CardInHand>().SetCard(topCard);
            cardInHandObject.name = topCard.Name;

            playerDeck.cards.RemoveAt(0);
            cardInHandObject.transform.SetParent(GameObject.Find("Hand").transform, false);
        }
    }

}
