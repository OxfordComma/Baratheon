using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardsInDeckCounter : MonoBehaviour {
	public Text cardsInDeckText;
	Player localPlayer;

	// Use this for initialization
	void Start () {
		localPlayer = GameController.GetGameController().localPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		//cardsInDeckText.text = localPlayer.deck.cards.Count.ToString() + "/30";
	}
}
