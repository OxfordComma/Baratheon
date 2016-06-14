﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckBuildingWindow : MonoBehaviour {
	CardHandler cardHandler;
	PlayerHandler playerHandler;
    public GameObject cardCollectionItemPrefab;
    public GameObject cardDeckItemPrefab;
	public ContentSizeFitter setList, deckList;


    // Use this for initialization
    void Start () {
		cardHandler = GameObject.Find ("Card Handler").GetComponent<CardHandler>();
		playerHandler = GameObject.Find ("Player Handler").GetComponent<PlayerHandler> ();
		List<Card> setCards = cardHandler.allCards;
        foreach (Card card in setCards)
        {
            GameObject collectionListItemObj = Instantiate(cardCollectionItemPrefab);
            CardListItem collectionListItem = collectionListItemObj.GetComponent<CardListItem>();
            collectionListItem.SetCard(card);

            collectionListItemObj.transform.SetParent(setList.transform);
        }

		List<Card> playerCards = playerHandler.currentPlayer.deck.cards;
		foreach (Card card in playerCards) 
		{
			GameObject deckListItemObj = Instantiate (cardDeckItemPrefab);
			CardListItem deckListItem = deckListItemObj.GetComponent<CardListItem> ();
            deckListItem.SetCard (card);

            deckListItem.transform.SetParent (deckList.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}	