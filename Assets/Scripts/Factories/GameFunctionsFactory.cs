using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class GameFunctionsFactory
{
    public GameObject GetObjectUnderMouse()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 mousePos = Input.mousePosition;
        Debug.DrawLine(mousePos, Vector2.zero, Color.cyan);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);
		if (hit == null)
			return null;
		
        return hit.transform.gameObject;
    }

    public void Draw(NetworkPlayer player, int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            Deck playerDeck = player.deck;
            Card topCard = playerDeck.cards[0];

            GameObject cardInHandObject = GameObject.Instantiate(Resources.Load("Prefabs/Battlefield/CardInHand")) as GameObject;
            cardInHandObject.GetComponent<CardInHand>().SetCard(topCard);
            cardInHandObject.name = topCard.name;

            playerDeck.cards.RemoveAt(0);
            cardInHandObject.transform.SetParent(GameObject.Find("Hand").transform, false);
        }
    }

}
