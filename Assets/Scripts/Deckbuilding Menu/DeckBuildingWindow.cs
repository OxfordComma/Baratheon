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

		List<Card> setCards = GameController.GetGameController ().set.cards;
		Dictionary<string, Card> setCardsDict = new Dictionary<string, Card> ();

        foreach (Card card in setCards)
        {
			setCardsDict.Add (card.name, card);

            GameObject setListItemObj = Instantiate(setListItemPrefab);
            SetListItem setListItem = setListItemObj.GetComponent<SetListItem>();
            setListItem.SetCard(card);
            setListItemObj.transform.SetParent(setList.transform);
        }

		string[] playerCards = GameController.GetLocalPlayer ().deckSyncList.ToArray ();

		foreach (string cardString in playerCards) 
		{
			Card card = setCardsDict [cardString];
			GameObject deckListItemObj = Instantiate (deckListItemPrefab);
			DeckListItem deckListItem = deckListItemObj.GetComponent<DeckListItem> ();
			deckListItem.SetCard (card);
            deckListItem.transform.SetParent (deckList.transform);
		}

		GameObject.Find("CardCounter").GetComponent<Text>().text = 
			GameController.GetLocalPlayer().deckSyncList.Count.ToString() + "/30";
	}
}	