﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardsInDeckCounter : MonoBehaviour {
	Text cardsInDeckText;
	Player localPlayer;

	// Use this for initialization
	void Start () {
		localPlayer = GameController.GetGameController().localPlayer;
		cardsInDeckText = this.gameObject.GetComponent<Text> ();

		GameObject.Find("CardCounter").GetComponent<Text>().text = 
			GameController.GetGameController().localPlayer.deck.cards.Count.ToString() + "/30";
	}
	
	// Update is called once per frame
	void Update () {
		cardsInDeckText.text = localPlayer.deck.cards.Count.ToString() + "/30";
	}
}