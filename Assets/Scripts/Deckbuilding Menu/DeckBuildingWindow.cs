using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckBuildingWindow : MonoBehaviour {
	public Text cardsInDeckText;
	CardHandler cardHandler;
	PlayerHandler playerHandler;
    GameObject setListItemPrefab, deckListItemPrefab;
	GameObject setList, deckList;


    // Use this for initialization
    void Start ()
    {
        setListItemPrefab = Resources.Load("Prefabs/Deckbuilding/SetListItem") as GameObject;
        deckListItemPrefab = Resources.Load("Prefabs/Deckbuilding/DeckListItem") as GameObject;

        deckList = GameObject.Find("DeckListContent");
        setList = GameObject.Find("SetListContent");

		List<Card> setCards = GameController.GetGameController().AllCards;

        foreach (Card card in setCards)
        {
            GameObject setListItemObj = Instantiate(setListItemPrefab);
            SetListItem setListItem = setListItemObj.GetComponent<SetListItem>();
            setListItem.SetCard(card);
            setListItemObj.transform.SetParent(setList.transform);
        }

		List<Card> playerCards = GameController.GetGameController().localPlayer.deck.cards;

		foreach (Card card in playerCards) 
		{
			GameObject deckListItemObj = Instantiate (deckListItemPrefab);
			DeckListItem deckListItem = deckListItemObj.GetComponent<DeckListItem> ();
            deckListItem.SetCard (card);

            deckListItem.transform.SetParent (deckList.transform);
		}

		GameObject.Find("CardCounter").GetComponent<Text>().text = 
			GameController.GetGameController().localPlayer.deck.cards.Count.ToString() + "/30";
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}	