using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

public class NetworkPlayer : NetworkBehaviour {

	[SyncVar]
	public string playerName;

	public SyncListString deckSyncList;
    public SyncListString cardsInHandSyncList;

	public Player player;

	public void Awake()
	{
		deckSyncList = new SyncListString ();
        cardsInHandSyncList = new SyncListString();
	}

	public void Start()
	{
		DontDestroyOnLoad (this);
	}

	public override void OnStartLocalPlayer()
	{
		DontDestroyOnLoad (this);
		Debug.Log ("Starting local player");
 		string username = GameObject.Find("LoginName").GetComponent<Text>().text;

		CmdLoadPlayer (username);
        CmdSaveToXML();

        Navigation.StaticGoToMainMenu ();

	}

	public Deck deck
	{
		get { return player.deck; }
		set { player.deck = value; }
	}

	[Command]
	public void CmdLoadPlayer(string username)
	{
		if (!Directory.Exists (Path.Combine (Application.persistentDataPath, "users")))
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users"));

		string userPath = Path.Combine (Application.persistentDataPath, "users").ToString ();
		string[] users = Directory.GetDirectories (userPath).Select (path => Path.GetFileName (path)).ToArray ();

		if (!users.Contains (username)) {
			Directory.CreateDirectory (Path.Combine (Application.persistentDataPath, "users/" + username));
			this.player = new Player (username, new Deck());
			player.SaveToXML ();
		}
		else {
			this.player = Player.Load (Path.Combine (Application.persistentDataPath, "users/" + username + "/player.xml"));
		}

		this.playerName = player.name;
		foreach (Card card in player.deck.cards)
			deckSyncList.Add (card.name);
		
		this.transform.name = player.name + " (Network)";
	}


    public void StartBattle()
    {
        Draw(3);
    }

	[Command]
	public void CmdAddCardToDeck(Card card)
	{
		deck.AddCard(card);
	}

	[Command]
	public void CmdRemoveCardFromDeck(Card card)
	{
		deck.RemoveCard (card);
	}

    public void CmdShuffleDeck()
    {
        int n = deckSyncList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(1, n + 1);
            string value = deckSyncList[k];
            deckSyncList[k] = deckSyncList[n];
            deckSyncList[n] = value;
        }
    }

    public void Draw(int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            GameObject cardInHandObject = GameObject.Instantiate(Resources.Load("Prefabs/Battlefield/CardInHand")) as GameObject;
            cardInHandObject.GetComponent<CardInHand>().SetCard(GameController.GetGameController().set.GetCard(deckSyncList[0]));
            cardInHandObject.name = deckSyncList[0];
            cardInHandObject.transform.SetParent(GameObject.Find("Hand").transform, false);
            cardsInHandSyncList.Add(deckSyncList[0]);
            deckSyncList.Remove(deckSyncList[0]);
        }
    }

	public bool CanAddCardToDeck(Card card)
	{
		return true;
	}

    [Command]
	public void CmdSaveToXML()
	{
		player.SaveToXML ();
	}
}
