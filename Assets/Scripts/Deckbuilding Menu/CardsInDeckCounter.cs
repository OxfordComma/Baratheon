using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardsInDeckCounter : MonoBehaviour {
	public Text cardsInDeckText;
	Player currentPlayer;

	// Use this for initialization
	void Start () {
		currentPlayer = GameObject.Find ("Player Handler").GetComponent<PlayerHandler> ().currentPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		cardsInDeckText.text = currentPlayer.deck.cards.Count.ToString() + "/30";
	}
}
