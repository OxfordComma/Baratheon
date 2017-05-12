using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckBuildingWindow : MonoBehaviour {
	public Text cardsInDeckText;
	//CardHandler cardHandler;
	//PlayerHandler playerHandler;
    public GameObject setListItemPrefab, deckListItemPrefab;
	public GameObject setListWindow, deckListWindow;

    // Use this for initialization
    void Awake ()
    {
		List<Card> setCards = GameController.GetGameController ().set.cards;
		Dictionary<string, Card> setCardsDict = new Dictionary<string, Card> ();

        foreach (Card card in setCards)
        {
			setCardsDict.Add (card.name, card);

            GameObject setListItemObj = Instantiate(setListItemPrefab);

            SetListItem setListItem = setListItemObj.GetComponent<SetListItem>();
            setListItem.SetCard(card);
            setListItemObj.transform.SetParent(setListWindow.transform);

        }

        string[] playerCards = NetworkController.GetNetworkPlayer().deck.ToStringArray();

        foreach (string cardString in playerCards)
        {
            Card card = setCardsDict[cardString];
            GameObject deckListItemObj = Instantiate(deckListItemPrefab);
            DeckListItem deckListItem = deckListItemObj.GetComponent<DeckListItem>();
            deckListItem.SetCard(card);
            deckListItem.transform.SetParent(deckListWindow.transform);
        }

        GameObject.Find("CardCounter").GetComponent<Text>().text =
            GameController.GetLocalPlayer().deck.cards.Count.ToString() + "/30";
    }
}	